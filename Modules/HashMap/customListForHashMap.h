#pragma once

#include <stdbool.h>
#include <stdlib.h>
#include "typeDef.h"

typedef struct List List;

//creates an empty list
List *create();

//inserts the node which has that index(indexes starts from 0)
int insertNode(List *list, Type value, int index);

void printList(List *list);

//inserts node to end
int insertNodeToEnd(List *list, Type value);

//deletes the node which has that index(indexes starts from 0)
Type deleteNode(List *list, int index);

//deletes last node
Type deleteLastNode(List *list);

//changes the value of the node which has that index(indexes starts from 0)
int changeNode(List *list, int index, Type value);

//changes value.value by 1
int changeNodeValueByOne(List *list, int index);

//checks if the list is empty
bool isListEmpty(List *list);

//clears the list
void clear(List *list);

//finds the value of the node which has that index(indexes starts from 0)
Type findNode(List *list, int index, int *errorCode);

//finds the index of the node which has that value(indexes starts from 0)
int findNodeIndexByValue(List *list, Type value);