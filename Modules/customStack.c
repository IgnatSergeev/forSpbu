#include "customStack.h"
#include <stdio.h>
#include <malloc.h>
#include <stdbool.h>

int push(Node **head, int value) {
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

int pop(Node **head, int *errorCode) {
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

bool isEmpty(Node *head) {
    return head == NULL;
}

void clear(Node **head) {
    while (!isEmpty(*head)) {
        int errorCode = 0;
        pop(head, &errorCode);
    }
}

int main() {
    Node *head = NULL;
    int errorCode = 0;

    if (push(&head, 100) != 0) {
        printf("Problems with memory allocation");
        return -1;
    }

    printf("%d\n", head->value);
    if (push(&head, 200) != 0) {
        printf("Problems with memory allocation");
        return -1;
    }
    printf("%d\n", head->value);

    int value = pop(&head, &errorCode);
    if (errorCode != 0) {
        printf("Trying to remove null element");
        return -1;
    }

    printf("%d, %d\n", head->value, value);

    printf("%d\n", isEmpty(head));
    clear(&head);

    printf("%d\n", isEmpty(head));
}