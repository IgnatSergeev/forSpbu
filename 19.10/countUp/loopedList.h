#pragma  once

#include <stdbool.h>
#include <stdlib.h>

typedef struct LoopedList LoopedList;

LoopedList *createLoopedList();

int insertNode(LoopedList *loopedList);

void printLoopedList(LoopedList *loopedList);

int deleteNode(LoopedList *loopedList, int position);

void clearLoopedList(LoopedList *loopedList);

bool isLoopedListEmpty(LoopedList *loopedList);

int loopedListSize(LoopedList *loopedList);