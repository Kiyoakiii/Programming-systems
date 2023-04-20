#include <stdio.h>
#include <stdlib.h>
#include <assert.h>
#include <stdbool.h>
#include <time.h>
//#include <dmalloc.h>





size_t strlen(const char* str) {
    const char* s = str;
    while (*s)
        ++s;
    return s - str;
}

char* strcpy(char* dest, const char* src) {
    char* p = dest;
    while (*src != '\0') {
        *p++ = *src++;
    }
    *p = '\0';
    return dest;
}

int strcmp(const char* str1, const char* str2) {
    size_t i = 0;
    while (str1[i] != '\0' && str2[i] != '\0') {
        if (str1[i] > str2[i]) {
            return 1;
        } else if (str1[i] < str2[i]) {
            return -1;
        }
        ++i;
    }
    if (str1[i] == '\0' && str2[i] == '\0') {
        
        return 0;
    } else if (str1[i] == '\0') {
        return -1;
    } else {
        return 1;
    }
}



typedef struct node {
    char key[257];  
    unsigned long long int value;  
    bool color;  
    struct node* left;  
    struct node* right;  
    struct node* parent;  
} Node;

typedef struct tree {
    Node *root;             
    Node *sentinel;         
} Tree;




void printTree(Tree* tree, Node *t, int depth)
{
    if (t == tree->sentinel) {
        return;
    }
    for (int i = 0; i < depth; i++) {
        printf("\t");
    }
    if (t->color == false) {
        printf("\033[91m%s:%llu\033[0m\n", t->key, t->value);
    } else {
        printf("%s:%llu\n", t->key, t->value);
    }
    printTree(tree, t->left, depth + 1);
    printTree(tree, t->right, depth + 1);
}



Tree* createTree();  
Node* createNode(Tree* tree, const char* key, unsigned long long int value);  
void destroy(Node* root, Node* sentinel);  
void destroyNode(Node* node);  
int insertNode(Tree* tree, const char* key, unsigned long long int value);  
int  deleteNode(Tree* tree, const char* key);  
Node* findNode(Tree* tree, const char* key);  


Tree* createTree() {
    Tree* tree = (Tree*)malloc(sizeof(Tree));
    if (tree == NULL) {
        printf("ERROR: Failed to allocate memory for tree\n");
        return NULL;
    }

    tree->sentinel = (Node*)malloc(sizeof(Node));
    if (tree->sentinel == NULL) {
        printf("ERROR: Failed to allocate memory for sentinel\n");
        free(tree);
        return NULL;
    }

    tree->sentinel->color = true;
    tree->sentinel->left = NULL;
    tree->sentinel->right = NULL;
    tree->sentinel->parent = NULL;

    tree->root = tree->sentinel;

    return tree;
}

void destroy(Node* root, Node* sentinel)
{
	if (root == sentinel){
        return  ;
    }
	destroy(root->left, sentinel);
	destroy(root->right, sentinel);
	free(root);
}


Node* createNode(Tree* tree, const char* key, unsigned long long int value) {
    Node* node = (Node*)malloc(sizeof(Node));
    if (node == NULL) {
        printf("ERROR: Failed to allocate memory for node\n");
        return NULL;
    }

    node->color = false;
    node->left = tree->sentinel;
    node->right = tree->sentinel;
    node->parent = NULL;
    strcpy(node->key, key);
    node->key[256] = '\0';
    node->value = value;

    return node;
}


void leftRotate(Tree* tree, Node* node)
{
    
	Node *X = node->right;
	X->parent = node->parent;
	if (node->parent == NULL){

		tree->root = X;
    }
	if (node->parent != NULL)
	{
		if (node->parent->left == node)
        {
            node->parent->left = X;
        }
		else{
            node->parent->right = X;
            }
	}
	node->right = X->left;
	if (X->left != tree->sentinel){
		X->left->parent = node;

    }
	node->parent = X;
	X->left = node;
}

void rightRotate(Tree* tree, Node* node) {
    
	Node *X = node->left;
	X->parent = node->parent;

	if (node->parent == NULL)
		tree->root = X;

	if (node->parent != NULL)
	{
		if (node->parent->left == node)
			node->parent->left = X;
		else
			node->parent->right = X;
	}

	node->left = X->right;
	if (X->right != tree->sentinel)
		X->right->parent = node;

	node->parent = X;
	X->right = node;
}

void transplant(Tree* tree, Node* u, Node* v) {
    if (u == NULL || v == NULL) {
        
        return;
    }
    
    if (u->parent != NULL){
        if (u == u->parent->left) {
            
            u->parent->left = v;
        } else {
            
            u->parent->right = v;
        }   
    } else {
        
        tree->root = v;
    }
    
    v->parent = u->parent;
    
}

Node* minimumNode(Node* node, Node* sentinel) {
    while (node->left != sentinel) {
        node = node->left;
    }
    return node;
}




void deleteFixup(Tree* tree, Node* x)
{
    Node* s;
	if (x->parent != NULL)
	{
		s = (x == x->parent->left) ? x->parent->right : x->parent->left;
		if (s->color == false)
		{
			x->parent->color = false;
			s->color = true;
			if (x == x->parent->left){
				leftRotate(tree, x->parent);
            }
			else{

				rightRotate(tree, x->parent);
            }
		}
		if (s->color == true && x->parent->color == true 
			&& s->right->color == true && s->left->color == true )
		{
			s->color = false;
			deleteFixup(tree, x->parent);
		} else{

			s = (x == x->parent->left) ? x->parent->right : x->parent->left;
			if (x->parent->color == false && s->color == true
				&& s->left->color == true && s->right->color == true)
			{
				s->color = false;
				x->parent->color = true;
			} else{
				if (s->color == true)
				{
					if ((x == x->parent->left) &&
						(s->left->color == false) &&
						(s->right->color == true))
					{
						s->color = false;
						s->left->color = true;
						rightRotate(tree, s);
					}
					else if ((x == x->parent->right) &&
						(s->right->color == false) &&
						(s->left->color == true))
					{
						s->color = false;
						s->right->color = true;
						leftRotate(tree, s);
					}
				}
				s = (x == x->parent->left) ? x->parent->right : x->parent->left;
				s->color = x->parent->color;
				x->parent->color = true;
				if (x == x->parent->left)
				{
					s->right->color = true;
					leftRotate(tree, x->parent);
				} else {
					s->left->color = true;
					rightRotate(tree, x->parent);
					}
				}
		}
		return;
	}
	tree->root = x;
}


int deleteNode(Tree* tree, const char* key) {
    
    Node* delnode = findNode(tree, key);
    if (delnode == NULL) {
        return 3;
    }
    Node* x;
    Node* y = delnode;
    
    bool y_original_color = y->color;
    if (delnode->left == tree->sentinel) {
        
        x = delnode->right;
        transplant(tree, delnode, delnode->right);
        
    }
    else if (delnode->right == tree->sentinel) {
        x = delnode->left;
        
        transplant(tree, delnode, delnode->left);
    }
    else {
        
        y = minimumNode(delnode->right, tree->sentinel);
        
        y_original_color = y->color;
        x = y->right;
        if (y->parent == delnode) {
            x->parent = y;
        }
        else {
            
            transplant(tree, y, y->right);
            y->right = delnode->right;
            y->right->parent = y;
        }
        
        transplant(tree, delnode, y);
        y->left = delnode->left;
        y->left->parent = y;
        y->color = delnode->color;
    }
    
    if (y_original_color == true) {
        deleteFixup(tree, x);
    }
    
    free(delnode);
    
    return 0;
}

void insertFixup(Tree* tree, Node* node) {
    Node* uncle;
    
    if (node->parent == NULL)
	{
        
		node->color = true;
        
		tree->root = node;
        
	}
    else{
        while (node->parent != NULL && node->parent->color == false) {
            
            if (node->parent == node->parent->parent->left) {
                
                Node* uncle = node->parent->parent->right;
                if (uncle->color == false) {
                    
                    
                    node->parent->color = true;
                    uncle->color = true;
                    node->parent->parent->color = false;
                    node = node->parent->parent;
                    
                } else {
                    
                    if (node == node->parent->right) {
                        
                        node = node->parent;
                        
                        leftRotate(tree, node);
                        
                    }
                    
                    node->parent->color = true;
                    node->parent->parent->color = false;
                    rightRotate(tree, node->parent->parent);
                    
                }
            } else {
                
                Node* uncle = node->parent->parent->left;
                if (uncle->color == false) {
                    
                    
                    node->parent->color = true;
                    uncle->color = true;
                    node->parent->parent->color = false;
                    node = node->parent->parent;
                    
                } else {
                    
                    if (node == node->parent->left) {
                        
                        node = node->parent;
                        
                        rightRotate(tree, node);
                        
                    }
                    
                    node->parent->color = true;
                    node->parent->parent->color = false;
                    
                    leftRotate(tree, node->parent->parent);
                    
                }
            }
            
        }
    }
    tree->root->color = true;
}


int insertNode(Tree* tree, const char* key, unsigned long long value) {
    

    Node* new_node = createNode(tree, key, value);
    if (new_node == NULL) {
        printf("ERROR: Unable to create a new node.\n");
        return 0;
    }
    
    
    Node* current = tree->root;
    Node* parent = NULL;
    while (current != tree->sentinel) {
        parent = current;
        
        if (strcmp(key, current->key) < 0) {
            current = current->left;
        }
        else if (strcmp(key, current->key) > 0)
			current = current->right;
		else  
			return 1;
    }
    
    new_node->parent = parent;
    if (parent == NULL) {
        tree->root = new_node;
        if (tree->root == NULL)
		{
			return 2;
		}

		tree->root->color = true;

		return 0;
    } 
    if (new_node == NULL)
	{
		return 2;
	}

    if (strcmp(key, parent->key) < 0) {
        parent->left = new_node;
    } else {
        parent->right = new_node;
    }

    insertFixup(tree, new_node);
    return 0;
}


Node* findNode(Tree* tree, const char* key) {
    Node* current = tree->root;
    
    while (current != tree->sentinel) {
        if (strcmp(key, current->key) == 0) {
            return current;
        } else if (strcmp(key, current->key) < 0) {
            current = current->left;
        } else {
            current = current->right;
        }
    }
    return NULL;
}

void to_lowercase(char *word) {
    for (size_t i = 0; word[i] != '\0' && i <= 256; ++i) {
        if (word[i] >= 'A' && word[i] <= 'Z') {
            word[i] += 'a' - 'A';
        }
    }
}

void printResult(int result)
{
	if (result == 0)
		printf("OK\n");
	if (result == 1)
		printf("Exist\n");
	if (result == 2)
		printf("ERROR: Cant allocate memory\n");
    if (result == 3)
		printf("NoSuchWord\n");
}

void printFileResult(FILE* outputFile, int result)
{
	if (result == 0)
		fprintf(outputFile, "OK\n");
	if (result == 1)
		fprintf(outputFile, "Exist\n");
	if (result == 2)
		fprintf(outputFile, "ERROR: Cant allocate memory\n");
    if (result == 3)
		fprintf(outputFile, "NoSuchWord\n");
}

void save(Tree* tree, Node* node, char* word, FILE* out)
{
	if (node == tree->sentinel)
		return;
	int temp = strlen(node->key);

	fwrite(&temp, sizeof(int), 1, out);
	fwrite(node->key, sizeof(char), temp, out);
	fwrite(&(node->value), sizeof(unsigned long long int), 1, out);

	save(tree, node->left, word, out);
	save(tree, node->right, word, out);

    return;
}

void load(Tree* tree, char* word, FILE* in)
{
    char buffer[257];
	unsigned long long int Key;
	int buferSize = 1;
	Tree* newTree = createTree();

	while (fread(&buferSize, sizeof(int), 1, in) == 1)
	{
        if (feof(in))
        {
            free(newTree->sentinel);

            return;
        } else if (ferror(in))
        {
            free(newTree->sentinel);

            return;
        }
		if (fread(buffer, sizeof(char), buferSize, in) != buferSize * sizeof(char))
		{
			if (feof(in))
			{
				free(newTree->sentinel);
				return;
			} else if (ferror(in))
			{
				free(newTree->sentinel);
				return;
			}
			destroy(newTree->root, newTree->sentinel);
			free(newTree->sentinel);
			return;
		}
		if (fread(&Key, sizeof(unsigned long long int), 1, in) != 1)
		{
			destroy(newTree->root, newTree->sentinel);
			free(newTree->sentinel);

			return;
		}
		buffer[buferSize] = '\0';
		insertNode(newTree, buffer, Key);
	}
	destroy(tree->root, tree->sentinel);
	free(tree->sentinel);
	tree->root = newTree->root;
	tree->sentinel = newTree->sentinel;
    return;
}


#define MAX_LINE_LENGTH 350


int main() {
    clock_t start, end;
    double cpu_time_used;
    
    char command[MAX_LINE_LENGTH+1];
    char arg1[MAX_LINE_LENGTH];
    unsigned long long number;
    Tree* tree = createTree();
    Node *node;
    FILE* inputFile = fopen("file.txt", "r");
    FILE* outputFile = fopen("output.txt", "w");
    int k = 1;
    while (fscanf(inputFile, "%s", command) > 0) {
        if (command[0] == '+'){
            fscanf(inputFile, "%s%llu", arg1, &number);
            to_lowercase(arg1);
            printFileResult(outputFile, insertNode(tree, arg1, number));
        } else if (command[0] == '-'){
            fscanf(inputFile, "%s", arg1);
            to_lowercase(arg1);
            printFileResult(outputFile, deleteNode(tree, arg1));
        } else if (command[0] == '!') {
            fscanf(inputFile, "%s", command);
            char filename[270];
            fscanf(inputFile, "%s", filename);
            if (command[0] == 'S') {
                FILE *out = fopen(filename, "wb");
                if (out != NULL) {
                    save(tree, tree->root, filename, out);
                    fclose(out);
                } 
                fprintf(outputFile, "OK\n");
                
            } else if (command[0] == 'L') {
                FILE *in = fopen(filename, "rb");
                if (in == NULL) {
                    destroy(tree->root, tree->sentinel);
                    tree->root = tree->sentinel;
                } else {
                    load(tree, filename, in);
                    fclose(in);
                }
                fprintf(outputFile, "OK\n");

            }

        } else {
            
            to_lowercase(command);
            node = findNode(tree, command);
            if (node == NULL){
                fprintf(outputFile, "NoSuchWord\n");
            }else{
                fprintf(outputFile, "OK: %llu\n", node->value);
            }
       
        }
        if (k == 2500000-100000){
            start = clock();
        }
        k++;
      
    }
    end = clock();

    cpu_time_used = ((double) (end - start)) / CLOCKS_PER_SEC;
    printf("%f sec\n", cpu_time_used);
    fclose(inputFile);
    fclose(outputFile);
    return 0;
}
