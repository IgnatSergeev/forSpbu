#pragma once

#include <stdbool.h>
#include <stdlib.h>
#include "../typeDef.h"

typedef struct BinaryTree BinaryTree;

BinaryTree *create();

bool compare(Type value1, Type value2);

Type whatIfEqual(Type value1, Type value2);

int addValue(BinaryTree *tree, Type value);

Type findValue(BinaryTree *tree, Type value, int *errorCode);

void clear(BinaryTree *tree);

void deleteValue(BinaryTree *tree, Type value);

bool isEmpty(BinaryTree *tree);