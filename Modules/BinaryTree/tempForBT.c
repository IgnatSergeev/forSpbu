#include <stdio.h>
#include "customBinaryTree.h"
#include <malloc.h>

int main() {
    BinaryTree *tree = create();
    addValue(tree, 10);
    addValue(tree, 5);
    addValue(tree, 3);
    addValue(tree, 8);
    int errorCode = 0;
    printf("%d", findValue(tree, 10, &errorCode));

    deleteValue(tree, 10);

    clear(tree);
    return 0;
}