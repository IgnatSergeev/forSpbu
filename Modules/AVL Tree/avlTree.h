#pragma once

#include "typeDef.h"
#include <stdio.h>
#include <stdbool.h>

typedef struct AVLTree AVLTree;

AVLTree *createAVLTree();

//compare: should return 0 if equal, 1 if first is grater than second and -1 if first is less than second
//whatIfEqualWhenAdding: first value - is old value; second value - is new value
int addValue(AVLTree *tree, Type value, int (*compare)(Type, Type), Type (*whatIfEqualInAdding)(Type, Type));

//whatIfEqualInSearching: first value - is old value; second value - is new value
Type findValue(AVLTree *tree, Type value, int *errorCode, Type zeroValue, int (*compare)(Type, Type), Type (*whatIfEqualInSearching)(Type, Type));

void clear(AVLTree *tree);

int deleteValue(AVLTree *tree, Type value, int (*compare)(Type, Type));

bool isEmpty(AVLTree *tree);