#pragma once

#include <stdbool.h>
#include <stdlib.h>

typedef struct BinaryTree BinaryTree;

BinaryTree *create();

int addNode(BinaryTree *tree, int value);

int findNode(BinaryTree *tree, int value, int *errorCode);

void clear(BinaryTree *tree);

int deleteNode(BinaryTree *tree, int value);

bool isEmpty(BinaryTree *tree);