#include "customStack.h"
#include <stdio.h>
#include <malloc.h>

struct Stack{
    int collection[100];
    int stackPosition;
};

int push(Stack *stack, int value) {
    if (stack->stackPosition + 1 >= 100) {
        return -1;
    }

    stack->stackPosition += 1;
    stack->collection[stack->stackPosition] = value;

    return 0;
}

int pop(Stack *stack, int *errorCode) {
    if (stack->stackPosition == -1) {
        if (errorCode != NULL) {
            *errorCode = -1;
        }

        return 0;
    }
    if (errorCode != NULL) {
        *errorCode = 0;
    }

    int value = stack->collection[stack->stackPosition];

    stack->stackPosition -= 1;

    return value;
}

bool isEmpty(Stack *stack) {
    return stack->stackPosition == -1;
}

void deleteStack(Stack *stack) {
    while (!isEmpty(stack)) {
        int errorCode = 0;
        pop(stack, &errorCode);
    }
    free(stack);
}

int top(Stack *stack) {
    return stack->collection[stack->stackPosition];
}

Stack* createStack() {
    Stack *stack = malloc(sizeof(Stack));
    stack->stackPosition = -1;
    return stack;
}