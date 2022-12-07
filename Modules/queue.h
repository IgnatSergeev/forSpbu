#pragma once
#include <stdbool.h>

typedef struct Node{
    int value;
    struct Node* next;
} Node;

typedef struct Queue{
    struct Node* head;
    struct Node* tail;
} Queue;

int enqueue(Queue *queue, int element);

int dequeue(Queue *queue, int *errorCode);

bool isEmpty(Queue *queue);

void clear(Queue *queue);

Queue *createQueue();