#include "customStack.h"
#include <stdio.h>
#include <malloc.h>

typedef struct Node{
    Type value;
    struct Node* next;
} Node;

struct Stack {
    Node* head;
};

int push(Stack *stack, Type value) {
    Node *temp = malloc(sizeof(Node));
    if (temp == NULL) {
        printf("Problems with memory allocation");
        return -1;
    }
    temp->value = value;
    temp->next = stack->head;

    stack->head = temp;
    return 0;
}

Type pop(Stack *stack, int *errorCode) {
    if (isStackEmpty(stack)) {
        if (errorCode != NULL) {
            *errorCode = -1;
        }

        return (Type)0;
    }
    if (errorCode != NULL) {
        *errorCode = 0;
    }

    Type value = stack->head->value;

    Node *next = stack->head->next;
    free(stack->head);
    stack->head = next;

    return value;
}

bool isStackEmpty(Stack *stack) {
    return stack->head == NULL;
}

void deleteStack(Stack *stack) {
    while (!isStackEmpty(stack)) {
        int errorCode = 0;
        pop(stack, &errorCode);
    }
    free(stack);
}

Type top(Stack *stack) {
    return stack->head->value;
}

Stack *createStack(void) {
    Stack *stack = malloc(sizeof(Stack));
    if (stack == NULL) {
        return stack;
    }
    stack->head = NULL;
    return stack;
}