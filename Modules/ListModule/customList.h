#pragma once

#include <stdbool.h>
#include <stdlib.h>
#include "typeDef.h"

typedef struct List List;

//creates an empty list
List *create();

//inserts the node which has that index(indexes starts from 0)
int insertNode(List *list, Type value, int index);

int insertNodeToEnd(List *list, Type value);

//prints list`s elements values
//void print(List *list);

//deletes the node which has that index(indexes starts from 0)
int deleteNode(List *list, int index);

//changes the value of the node which has that index(indexes starts from 0)
int changeNode(List *list, int index, Type value);

//checks if the list is empty
bool isEmpty(List *list);

//clears the list
void clear(List *list);

//finds the value of the node which has that index(indexes starts from 0)
Type findNode(List *list, int index, int *errorCode);

//finds the index of the node which has that value(indexes starts from 0)
int findNodeIndexByValue(List *list, Type value);