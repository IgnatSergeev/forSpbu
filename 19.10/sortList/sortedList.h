#pragma once

#include <stdbool.h>
#include <stdlib.h>

typedef struct SortedList SortedList;

SortedList *createSortedList();

int insertNode(SortedList *sortedList, int value);

void printSortedList(SortedList *sortedList);

int deleteNode(SortedList *sortedList, int value);

void clearSortedList(SortedList *sortedList);

bool isSortedListEmpty(SortedList *sortedList);