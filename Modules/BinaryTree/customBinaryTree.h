#pragma once

#include <stdbool.h>
#include <stdlib.h>
#include "../typeDef.h"

typedef struct BinaryTree BinaryTree;

BinaryTree *create();

//compare: should return 0 if equal, 1 if first is grater than second and -1 if first is less than second
//whatIfEqualWhenAdding: first value - is old value; second value - is new value
int addValue(BinaryTree *tree, Type value, int (*compare)(Type, Type), Type (*whatIfEqualInAdding)(Type, Type));

//whatIfEqualInSearching: first value - is old value; second value - is new value
Type findValue(BinaryTree *tree, Type value, int *errorCode, Type zeroValue, int (*compare)(Type, Type), Type (*whatIfEqualInSearching)(Type, Type));

bool isTheValueInTree(BinaryTree *tree, Type value, int *errorCode, int (*compare)(Type, Type));

void clear(BinaryTree *tree);

int deleteValue(BinaryTree *tree, Type value, int (*compare)(Type, Type));

bool isEmpty(BinaryTree *tree);