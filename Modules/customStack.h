#pragma once
#include <stdbool.h>

typedef struct Stack Stack;

int push(Stack *stack, int element);

int pop(Stack *stack, int *errorCode);

bool isEmpty(Stack *stack);

Stack* createStack(void);

void deleteStack(Stack *stack);

int top(Stack *stack);