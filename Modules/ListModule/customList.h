#pragma once

#include <stdbool.h>
#include <stdlib.h>
#include "typeDef.h"

typedef struct List List;

//creates an empty list
List *create();

//inserts the node which has that index(indexes starts from 0)
int insertNode(List *list, Type value, int index);

//inserts node to the end
int insertNodeToEnd(List *list, Type value);

//deletes the node which has that index(indexes starts from 0)
Type deleteNode(List *list, int index);

//deletes last node
Type deleteLastNode(List *list);

//changes the value of the node which has that index(indexes starts from 0)
int changeNode(List *list, int index, Type value);

//checks if the list is empty
bool isEmpty(List *list);

//clears the list
void clear(List *list);

//finds the value of the node which has that index(indexes starts from 0)
Type findNode(List *list, int index, int *errorCode, Type nullReturn);

//appends to the end of th list all string which starts with 'a'
int appendToEndStringsStartingWithA(List *list);
