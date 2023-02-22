#include <stdio.h>
#include "customBinaryTree.h"
#include <malloc.h>

Type whatIfEqualWhenAdding(Type oldValue, Type newValue) {
    oldValue.value = newValue.value;
    return oldValue;
}

Type whatIfEqualWhenSearching(Type oldValue, Type newValue) {
    return oldValue;
}

int compare(Type oldValue, Type newValue) {
    return (oldValue.key == newValue.key) ? 0 : (oldValue.key < newValue.key) ? -1 : 1;
}

int main() {
    BinaryTree *tree = create();
    Type value = {10, -1};
    addValue(tree, value, &compare, &whatIfEqualWhenAdding);
    value.key = 5;
    addValue(tree, value, &compare, &whatIfEqualWhenAdding);
    value.key = 3;
    addValue(tree, value, &compare, &whatIfEqualWhenAdding);
    value.key = 8;
    addValue(tree, value, &compare, &whatIfEqualWhenAdding);
    value.key = 3;
    value.value = 20;
    addValue(tree, value, &compare, &whatIfEqualWhenAdding);
    Type zeroValue = {0, 0};
    int errorCode = 0;
    value.value = 15;
    Type returnValue = findValue(tree, value, &errorCode, zeroValue, &compare, &whatIfEqualWhenSearching);
    printf("%d", returnValue.value);
    value.key = 2;
    returnValue = findValue(tree, value, &errorCode, zeroValue, &compare, &whatIfEqualWhenSearching);
    printf("%d", returnValue.value);

    value.key = 5;
    deleteValue(tree, value, &compare);

    clear(tree);
    return 0;
}