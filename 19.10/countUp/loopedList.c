#include "loopedList.h"

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

void clearSortedList(LoopedList *loopedList) {
    while (!isLoopedListEmpty(loopedList)) {
        deleteNode(loopedList, 0);
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
        newNode->realPosition = 0;
        newNode->startPosition = 0;
        newNode->next = newNode;
        loopedList->head = newNode;
        return 0;
    }

    Node *lastNode = loopedList->head;
    while (lastNode->next != NULL) {
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

    if (headNode->realPosition == realPositionToDelete) {
        if (loopedList->size == 1) {
            free(headNode);
            loopedList->head = NULL;
            loopedList->size = 0;
            return 0;
        }
        Node *iteratorNode = headNode;
        while (iteratorNode->next != headNode) {
            iteratorNode = iteratorNode->next;
            iteratorNode->realPosition -= 1;
        }
        Node *lastNode = iteratorNode;
        lastNode->next = headNode->next;
        free(headNode);
        loopedList->size -= 1;
        return 0;
    }

    Node *iteratorNode = headNode;
    while (iteratorNode->next->realPosition != realPositionToDelete) {
        iteratorNode = iteratorNode->next;
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
