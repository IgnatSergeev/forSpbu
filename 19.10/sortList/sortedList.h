#pragma once

#include <stdbool.h>
#include <stdlib.h>

typedef struct SortedList SortedList;

int insertNode(SortedList *sortedList, int value);

void print(SortedList *sortedList);

int deleteNode(SortedList *sortedList, int position);

int clear(SortedList *sortedList, int position);

bool isEmpty(SortedList *sortedList);