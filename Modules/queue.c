#include "queue.h"
#include <stdio.h>
#include <malloc.h>

int enqueue(Queue *queue, int value) {
    Node *temp = malloc(sizeof(Node));
    if (temp == NULL) {
        printf("Problems with memory allocation");
        return -1;
    }
    temp->value = value;
    temp->next = NULL;

    if (queue->tail == NULL) {
        queue->tail = temp;
        queue->head = temp;
    } else {
        queue->tail->next = temp;
        queue->tail = temp;
    }

    return 0;
}

int dequeue(Queue *queue, int *errorCode) {
    if (queue->head == NULL) {
        if (errorCode != NULL) {
            *errorCode = -1;
        }

        return 0;
    }
    if (errorCode != NULL) {
        *errorCode = 0;
    }

    int value = queue->head->value;

    Node *next = queue->head->next;
    free(queue->head);
    queue->head = next;
    if (next == NULL) {
        queue->tail = NULL;
    }

    return value;
}

bool isEmpty(Queue queue) {
    return queue.head == NULL;
}

void clear(Queue *queue) {
    while (!isEmpty(*queue)) {
        int errorCode = 0;
        dequeue(queue, &errorCode);
    }
}

int main() {
    Queue queue = {NULL, NULL};
    int errorCode = 0;

    if (enqueue(&queue, 100) != 0) {
        printf("Problems with memory allocation");
        return -1;
    }

    printf("%d, %d\n", queue.head->value, queue.tail->value);
    if (enqueue(&queue, 200) != 0) {
        printf("Problems with memory allocation");
        return -1;
    }
    printf("%d, %d\n", queue.head->value, queue.tail->value);

    int value = dequeue(&queue, &errorCode);
    if (errorCode != 0) {
        printf("Trying to remove null element");
        return -1;
    }

    printf("%d, %d, %d\n", queue.head->value, queue.tail->value, value);

    printf("%d\n", isEmpty(queue));
    clear(&queue);
    printf("%d\n", isEmpty(queue));

    if (enqueue(&queue, 100) != 0) {
        printf("Problems with memory allocation");
        return -1;
    }
    value = dequeue(&queue, &errorCode);
    if (errorCode != 0) {
        printf("Trying to remove null element");
        return -1;
    }
    printf("%d, %d\n", queue.head->value, queue.tail->value);
}
