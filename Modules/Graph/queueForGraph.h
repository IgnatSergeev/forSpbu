#pragma once
#include <stdbool.h>

typedef struct Queue Queue;

Queue *createQueue();

int enqueue(Queue *queue, int element);

int dequeue(Queue *queue, int *errorCode);

bool isEmpty(Queue *queue);

void clear(Queue *queue);