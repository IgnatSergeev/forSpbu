#include "customStack.h"
#include <stdio.h>
#include <malloc.h>

typedef struct Node{
    int value;
    struct Node* next;
} Node;

struct Stack {
    Node* head;
};

int push(Stack *stack, int value) {
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

int pop(Stack *stack, int *errorCode) {
    if (stack == NULL) {
        if (errorCode != NULL) {
            *errorCode = -1;
        }

        return 0;
    }
    if (errorCode != NULL) {
        *errorCode = 0;
    }

    int value = stack->head->value;

    Node *next = stack->head->next;
    free(stack->head);
    stack->head = next;

    return value;
}

bool isEmpty(Stack *stack) {
    return stack->head == NULL;
}

void deleteStack(Stack *stack) {
    while (!isEmpty(stack)) {
        int errorCode = 0;
        pop(stack, &errorCode);
    }
    free(stack);
}

int top(Stack *stack) {
    return stack->head->value;
}

Stack *createStack() {
    Stack *stack = malloc(sizeof(Stack));
    return stack;
}