#include "sortedList.h"
#include <stdio.h>

typedef struct Node {
    int value;
    struct Node *next;
} Node;

struct SortedList {
    Node *head;
};

void printSortedList(SortedList *sortedList) {
    Node *temp = sortedList->head;

    while(temp->next != NULL)
    {
        printf("%d ", temp->value);
        temp = temp->next;
    }
    printf("%d\n", temp->value);
}

int insertNode(SortedList *sortedList, int value) {
    Node *currentNode = sortedList->head;
    Node *newNode = malloc(sizeof(Node));
    if (newNode == NULL) {
        return -1;
    }

    if (isSortedListEmpty(sortedList)) {
        newNode->value = value;
        newNode->next = NULL;
        sortedList->head = newNode;
        return 0;
    }

    while (currentNode->next != NULL && currentNode->next->value < value) {
        currentNode = currentNode->next;
    }
    newNode->value = value;
    newNode->next = currentNode->next;
    currentNode->next = newNode;
    return 0;
}

int popHead(SortedList *sortedList) {
    if (isSortedListEmpty(sortedList)) {
        return -1;
    }

    Node *head = sortedList->head;
    Node *newHead = head->next;

    sortedList->head = newHead;
    free(head);
    return 0;
}

int deleteNode(SortedList *sortedList, int value) {
    if (isSortedListEmpty(sortedList)) {
        return -1;
    }
    Node *currentNode = sortedList->head;
    while (currentNode->next != NULL && currentNode->next->value != value) {
        currentNode = currentNode->next;
    }
    if (currentNode->next == NULL) {
        return -1;
    }

    Node *nodeToDelete = currentNode->next;
    Node *nodeAfterTheOneToDelete = nodeToDelete->next;
    currentNode->next = nodeAfterTheOneToDelete;

    free(nodeToDelete);
    return 0;
}

bool isSortedListEmpty(SortedList *sortedList) {
    return sortedList->head == NULL;
}

void clearSortedList(SortedList *sortedList) {
    while (!isSortedListEmpty(sortedList)) {
        popHead(sortedList);
    }
    free(sortedList);
}

SortedList *createSortedList() {
    SortedList *sortedList = malloc(sizeof(SortedList));
    sortedList->head = NULL;

    return sortedList;
}