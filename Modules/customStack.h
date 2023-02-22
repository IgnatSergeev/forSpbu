#pragma once
#include <stdbool.h>
#include "typeDef.h"

typedef struct Stack Stack;

int push(Stack *stack, Type element);

Type pop(Stack *stack, int *errorCode);

bool isEmpty(Stack *stack);

Stack* createStack(void);

void deleteStack(Stack *stack);

Type top(Stack *stack);