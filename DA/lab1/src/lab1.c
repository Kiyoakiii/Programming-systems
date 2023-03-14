#include <stdio.h>
#include <stdlib.h>

const int kMaxPostalCode = 1000000;

struct values_on_the_sides {
    int left;
    int right;
};

struct KeyValuePair {
  int key;
  struct values_on_the_sides value;
};

void CountingSort(struct KeyValuePair* array, int n) {
    int* counts = (int*)calloc(kMaxPostalCode, sizeof(int));
    for (int i = 0; i < n; ++i) {
        ++counts[array[i].key];
    }
    for (int i = 1; i < kMaxPostalCode; ++i) {
        counts[i] += counts[i - 1];
    }
    struct KeyValuePair* sorted_array = (struct KeyValuePair*)malloc(kMaxPostalCode * sizeof(struct KeyValuePair));

    for (int i = n - 1; i >= 0; --i) {
        sorted_array[--counts[array[i].key]] = array[i];
    }
    for (int i = 0; i < n; ++i) {
        array[i] = sorted_array[i];
    }
    free(sorted_array);
}

int main() {
    char* data_of_key_value = (char*) malloc(1e8 * sizeof(char));
    for (int i = 0; i < 10000; ++i) {
        data_of_key_value[i] = 0;
    }

    struct KeyValuePair *arr = (struct KeyValuePair *) malloc(1e7*sizeof(struct KeyValuePair));
    int idx = 0;
    int len = 0;
	struct KeyValuePair one_str;
    char value[2049] = {0};
    int i = 0;
    int key = 0;
    int left, right;

    while (scanf("%d %s", &one_str.key, value) != EOF) {
        int key = 0;
        left = len;
        right = len;
        for (int i = 0; i <= 2048 && value[i] != 0; ++i) {
            data_of_key_value[left+i] = value[i];
            ++right;
            value[i] = 0;
        }

        len += right - left + 1;
        one_str.value.left = left;
        one_str.value.right = right;
        arr[idx] = one_str;
        ++idx;
    }
    int n = idx;
    CountingSort(arr, n);
    for (int i = 0; i < idx; ++i) {
        printf("%06d\t",arr[i].key);
        for (int j = arr[i].value.left; j < arr[i].value.right; ++j) {
            printf("%c", data_of_key_value[j]);
        }
        printf("\n");
    }
    free(data_of_key_value);
    free(arr);
    return 0;
}
