#pragma once
#include <stdbool.h>

//Элемент стека
struct Node;

typedef struct Node* Stack;

int push(Stack *head, int element);

int pop(Stack *head, int *errorCode);

bool isEmpty(Stack head);

Stack createStack();

void deleteStack(Stack *head);

int top(Stack head);