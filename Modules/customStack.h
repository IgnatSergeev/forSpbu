#pragma once
#include <stdbool.h>

typedef struct Node{
    int value;
    struct Node* next;
} Node;

int push(Node **head, int element);

int pop(Node **head, int *errorCode);

bool isEmpty(Node *head);

void clear(Node **head);
