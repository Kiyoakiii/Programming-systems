import random

def generate_test(num_add, num_delete, num_search):
    commands = []
    for i in range(num_add):
        word = ''.join(random.choices('abcdefghijklmnopqrstuvwxyz', k=10))
        num = random.randint(0, 2**64-1)
        commands.append(f"+ {word} {num}")
    for i in range(num_delete):
        word = ''.join(random.choices('abcdefghijklmnopqrstuvwxyz', k=10))
        commands.append(f"- {word}")
    for i in range(num_search):
        word = ''.join(random.choices('abcdefghijklmnopqrstuvwxyz', k=10))
        commands.append(word)
    
    with open("file.txt", "w") as f:
        f.write("\n".join(commands))

    f.close()

with open("file.txt", "w") as f:
    f.write("")
f.close()
num_line = 2500000

num_delete = 0
num_add = num_line - 100000
num_search = 100000

generate_test(num_add, num_delete, num_search)
