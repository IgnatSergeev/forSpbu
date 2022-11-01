#pragma once

#include <stdbool.h>
#include <stdlib.h>

typedef struct SortedList SortedList;

SortedList *create();

int insertNode(SortedList *sortedList, int value);

void print(SortedList *sortedList);

int deleteNode(SortedList *sortedList, int value);

void clear(SortedList *sortedList);

bool isEmpty(SortedList *sortedList);