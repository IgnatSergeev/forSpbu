#include "customList.h"
#include <stdio.h>

typedef struct Node {
    Type value;
    struct Node *next;
} Node;

struct List {
    Node *head;
    int listSize;
};

int insertNode(List *list, Type value, int index) {
    if (index < 0) {
        return -1;
    }
    if (index == 0) {
        Node *newNode = malloc(sizeof(Node));
        if (newNode == NULL) {
            return -1;
        }
        newNode->next = list->head;
        newNode->value = value;
        list->head = newNode;
        list->listSize += 1;
        return 0;
    }
    if (isEmpty(list)) {
        return -1;
    }

    Node *iteratorNode = list->head;
    int currentIndex = 0;
    while (currentIndex != index - 1) {
        if (iteratorNode->next == NULL) {
            return -1;
        }
        ++currentIndex;
        iteratorNode = iteratorNode->next;
    }

    Node *newNode = malloc(sizeof(Node));
    if (newNode == NULL) {
        return -1;
    }
    newNode->value = value;
    newNode->next = iteratorNode->next;
    iteratorNode->next = newNode;
    list->listSize += 1;
    return 0;
}

int insertNodeToEnd(List *list, Type value) {
    return insertNode(list, value, list->listSize);
}

List *create() {
    List *list = calloc(1, sizeof(List));
    if (list == NULL) {
        return NULL;
    }
    list->head = NULL;

    return list;
}

Type deleteNode(List* list, int index) {
    if (isEmpty(list)) {
        return NULL;
    }
    if (index < 0) {
        return NULL;
    }
    if (index == 0) {
        Node *nodeToDelete = list->head;
        list->head = nodeToDelete->next;
        list->listSize -= 1;
        Type value = nodeToDelete->value;
        free(nodeToDelete);
        return value;
    }

    Node *iteratorNode = list->head;
    int currentIndex = 0;
    while (currentIndex != index - 1) {
        if (iteratorNode->next == NULL) {
            return NULL;
        }
        ++currentIndex;
        iteratorNode = iteratorNode->next;
    }
    Node *nodeToDelete = iteratorNode->next;
    iteratorNode->next = nodeToDelete->next;
    Type value = nodeToDelete->value;
    list->listSize -= 1;
    free(nodeToDelete);
    return value;
}

Type deleteLastNode(List *list) {
    return deleteNode(list, list->listSize - 1);
}

Type findNode(List *list, int index, int *errorCode, Type nullReturn) {
    if (isEmpty(list) || index < 0) {
        *errorCode = -1;
        return nullReturn;
    }
    Node *iteratorNode = list->head;

    for (int i = 0; i < index; ++i) {
        iteratorNode = iteratorNode->next;
        if (iteratorNode == NULL) {
            *errorCode = -1;
            return nullReturn;
        }
    }

    *errorCode = 0;
    return iteratorNode->value;
}

bool isEmpty(List *list) {
    return list->head == NULL;
}

void clear(List *list) {
    while (!isEmpty(list)) {
        deleteNode(list, 0);
    }
    free(list);
}

int changeNode(List *list, int index, Type value) {
    if (index < 0) {
        return -1;
    }
    if (isEmpty(list)) {
        return -1;
    }

    Node *iteratorNode = list->head;
    int currentIndex = 0;
    while (currentIndex != index) {
        if (iteratorNode->next == NULL) {
            return -1;
        }
        ++currentIndex;
        iteratorNode = iteratorNode->next;
    }

    iteratorNode->value = value;
    return 0;
}

int appendToEndStringsStartingWithA(List *list) {
    if (isEmpty(list)) {
        return 0;
    }
    Node *iteratorNode = list->head;
    while (iteratorNode->next != NULL) {
        iteratorNode = iteratorNode->next;
    }

    Node *endNode = iteratorNode;

    iteratorNode = list->head;
    while (iteratorNode != endNode) {
        if (iteratorNode->value != NULL && iteratorNode->value[0] == 'a') {
            int errorCode = insertNodeToEnd(list, iteratorNode->value);
            if (errorCode) {
                return 1;
            }
        }
        iteratorNode = iteratorNode->next;
    }
    if (iteratorNode->value != NULL && iteratorNode->value[0] == 'a') {
        int errorCode = insertNodeToEnd(list, iteratorNode->value);
        if (errorCode) {
            return 1;
        }
    }
    return 0;
}


