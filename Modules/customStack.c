#include "customStack.h"
#include <stdio.h>
#include <malloc.h>

typedef struct Stack{
    int value;
    struct Node* next;
} Stack;

int push(Stack *head, int value) {
    Node *temp = malloc(sizeof(Node));
    if (temp == NULL) {
        printf("Problems with memory allocation");
        return -1;
    }
    temp->value = value;
    temp->next = *head;

    *head = temp;
    return 0;
}

int pop(Stack *head, int *errorCode) {
    if (*head == NULL) {
        if (errorCode != NULL) {
            *errorCode = -1;
        }

        return 0;
    }
    if (errorCode != NULL) {
        *errorCode = 0;
    }

    int value = (*head)->value;

    Node *next = (*head)->next;
    free(*head);
    *head = next;

    return value;
}

bool isEmpty(Stack head) {
    return head == NULL;
}

void deleteStack(Stack *head) {
    while (!isEmpty(*head)) {
        int errorCode = 0;
        pop(head, &errorCode);
    }
}

int top(Stack head) {
    return head->value;
}

Stack createStack() {
    Stack stack = malloc(sizeof(Node));
    return stack;
}