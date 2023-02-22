#include "loopedList.h"
#include <stdio.h>

typedef struct Node {
    int realPosition;
    int startPosition;
    struct Node *next;
} Node;

struct LoopedList {
    Node *head;
    int size;
};

bool isLoopedListEmpty(LoopedList *loopedList) {
    return loopedList->head == NULL;
}

LoopedList *createLoopedList() {
    LoopedList *loopedList = malloc(sizeof(LoopedList));
    loopedList->head = NULL;
    loopedList->size = 0;

    return loopedList;
}

void clearLoopedList(LoopedList *loopedList) {
    while (!isLoopedListEmpty(loopedList)) {
        deleteNode(loopedList, 1);
    }
    free(loopedList);
}

int insertNode(LoopedList *loopedList) {
    Node *newNode = malloc(sizeof(Node));
    if (newNode == NULL) {
        return -1;
    }
    loopedList->size += 1;
    if (isLoopedListEmpty(loopedList)) {
        newNode->realPosition = 1;
        newNode->startPosition = 1;
        newNode->next = newNode;
        loopedList->head = newNode;
        return 0;
    }

    Node *headNode = loopedList->head;
    Node *lastNode = headNode;
    while (lastNode->next != headNode) {
        lastNode = lastNode->next;
    }
    newNode->realPosition = lastNode->realPosition + 1;
    newNode->startPosition = lastNode->startPosition + 1;
    newNode->next = lastNode->next;
    lastNode->next = newNode;
    return 0;
}

int deleteNode(LoopedList *loopedList, int realPositionToDelete) {
    if (isLoopedListEmpty(loopedList)) {
        return -1;
    }
    Node *headNode = loopedList->head;

    if (loopedList->size == 1) {
        if (headNode->realPosition != realPositionToDelete) {
            return -1;
        }
        free(headNode);
        loopedList->head = NULL;
        loopedList->size = 0;
        return 0;
    }

    if (headNode->realPosition == realPositionToDelete) {
        Node *iteratorNode = headNode;
        while (iteratorNode->next != headNode) {
            iteratorNode = iteratorNode->next;
            iteratorNode->realPosition -= 1;
        }
        Node *lastNode = iteratorNode;
        lastNode->next = headNode->next;
        loopedList->head = headNode->next;
        free(headNode);
        loopedList->size -= 1;
        return 0;
    }

    Node *iteratorNode = headNode;
    while (iteratorNode->next != headNode && iteratorNode->next->realPosition != realPositionToDelete) {
        iteratorNode = iteratorNode->next;
    }
    if (iteratorNode->next == headNode) {
        return -1;
    }
    Node *nodeToDelete = iteratorNode->next;
    iteratorNode->next = nodeToDelete->next;
    free(nodeToDelete);
    while (iteratorNode->next != headNode) {
        iteratorNode = iteratorNode->next;
        iteratorNode->realPosition -= 1;
    }
    loopedList->size -= 1;
    return 0;
}

void printLoopedList(LoopedList *loopedList) {
    if (!isLoopedListEmpty(loopedList)) {
        Node *headNode = loopedList->head;
        Node *iteratorNode = headNode;

        while(iteratorNode->next != headNode)
        {
            printf("%d ", iteratorNode->startPosition);
            iteratorNode = iteratorNode->next;
        }
        printf("%d\n", iteratorNode->startPosition);
    }
}

int loopedListSize(LoopedList *loopedList) {
    return loopedList->size;
}

int top(LoopedList *loopedList, int *errorCode) {
    if (loopedList->head == NULL) {
        *errorCode = 1;
        return 0;
    }
    return loopedList->head->startPosition;
}