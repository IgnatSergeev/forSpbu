#pragma once

#include <stdbool.h>
#include <stdlib.h>

typedef struct BinaryTree BinaryTree;

BinaryTree *create();

int addValue(BinaryTree *tree, int value);

int findValue(BinaryTree *tree, int value, int *errorCode);

void clear(BinaryTree *tree);

void deleteValue(BinaryTree *tree, int value);

bool isEmpty(BinaryTree *tree);