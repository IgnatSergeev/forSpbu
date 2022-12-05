#pragma once
#include <stdbool.h>
#include "typeDef.h"

typedef struct Stack Stack;

int push(Stack *stack, Type element);

Type pop(Stack *stack, int *errorCode);

bool isStackEmpty(Stack *stack);

//creates the stack
Stack* createStack(void);

//clears the stack(cannot be used after cleaning)
void deleteStack(Stack *stack);

//returns the value of the top element
Type top(Stack *stack);