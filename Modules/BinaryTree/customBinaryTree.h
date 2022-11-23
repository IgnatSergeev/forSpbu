#pragma once

#include <stdbool.h>
#include <stdlib.h>
#include "typeDef.h"

typedef struct BinaryTree BinaryTree;

//creates an empty binary tree
BinaryTree *create();

//compare: should return 0 if equal, 1 if first is grater than second and -1 if first is less than second
//whatIfEqualWhenAdding: first value - is old value; second value - is new value
int addValue(BinaryTree *tree, Type value, int (*compare)(Type, Type), Type (*whatIfEqualInAdding)(Type, Type));

//whatIfEqualInSearching: first value - is old value; second value - is new value
//compare: should return 0 if equal, 1 if first is grater than second and -1 if first is less than second
//zeroValue - the value which is returned in case the search is failed
Type findValue(BinaryTree *tree, Type value, int *errorCode, Type zeroValue, int (*compare)(Type, Type), Type (*whatIfEqualInSearching)(Type, Type));

//clears the tree(cannot be used after cleaning)
void clear(BinaryTree *tree);

//deletes element from the tree by value
//compare: should return 0 if equal, 1 if first is grater than second and -1 if first is less than second
int deleteValue(BinaryTree *tree, Type value, int (*compare)(Type, Type));

//checks id the tree is empty
bool isEmpty(BinaryTree *tree);

void treeTraversal(BinaryTree *binaryTree, Type array[]);