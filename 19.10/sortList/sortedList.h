#pragma once

#include <stdbool.h>
#include <stdlib.h>

typedef struct SortedList SortedList;

//creates sortedlist
SortedList *createSortedList();

//inserts value to the list
int insertNode(SortedList *sortedList, int value);

//prints the list
void printSortedList(SortedList *sortedList);

//deletes the node by value
int deleteNode(SortedList *sortedList, int value);

//clears the sorted list
void clearSortedList(SortedList *sortedList);

//checks if the list is empty
bool isSortedListEmpty(SortedList *sortedList);