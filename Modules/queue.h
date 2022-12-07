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

//Adds element to the top of the queue
int enqueue(Queue *queue, int element);

//Removes element to the bottom of the queue
int dequeue(Queue *queue, int *errorCode);

//Checks if the queue is empty
bool isEmpty(Queue *queue);

//Clears the queue - cannot be used again
void clear(Queue *queue);

//Creates empty queue
Queue *createQueue();