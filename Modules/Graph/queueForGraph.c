#include "queueForGraph.h"
#include <stdio.h>
#include <malloc.h>

typedef struct Node{
    int value;
    struct Node* next;
} Node;

struct Queue{
    struct Node* head;
    struct Node* tail;
};

Queue *createQueue() {
    Queue *queue = calloc(1, sizeof(Queue));
    return queue;
}

int enqueue(Queue *queue, int value) {
    Node *temp = malloc(sizeof(Node));
    if (temp == NULL) {
        return -1;
    }
    temp->value = value;
    temp->next = NULL;

    if (queue->tail == NULL) {
        queue->tail = temp;
        queue->head = temp;
    } else {
        queue->tail->next = temp;
        queue->tail = temp;
    }

    return 0;
}

int dequeue(Queue *queue, int *errorCode) {
    if (isEmpty(queue)) {
        if (errorCode != NULL) {
            *errorCode = -1;
        }

        return 0;
    }
    if (errorCode != NULL) {
        *errorCode = 0;
    }

    int value = queue->head->value;

    Node *next = queue->head->next;
    free(queue->head);
    queue->head = next;
    if (next == NULL) {
        queue->tail = NULL;
    }

    return value;
}

bool isEmpty(Queue *queue) {
    return queue->head == NULL;
}

void clear(Queue *queue) {
    while (!isEmpty(queue)) {
        int errorCode = 0;
        dequeue(queue, &errorCode);
    }
}

