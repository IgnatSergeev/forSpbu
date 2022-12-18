#include <stdio.h>
#include "typeDef.h"
#include <malloc.h>
#include <stdbool.h>

typedef struct Edge {
    EdgeProperties value;
    struct Edge *next;
} Edge;

typedef struct List {
    Edge *head;
    int listSize;
    NodeData nodeData;
} List;

bool isEmpty(List *list) {
    return list->head == NULL;
}

int insertNode(List *list, EdgeProperties value, int index) {
    if (index < 0) {
        return -1;
    }
    if (index == 0) {
        Edge *newNode = malloc(sizeof(Edge));
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

    Edge *iteratorNode = list->head;
    int currentIndex = 0;
    while (currentIndex != index - 1) {
        if (iteratorNode->next == NULL) {
            return -1;
        }
        ++currentIndex;
        iteratorNode = iteratorNode->next;
    }

    Edge *newNode = malloc(sizeof(Edge));
    if (newNode == NULL) {
        return -1;
    }
    newNode->value = value;
    newNode->next = iteratorNode->next;
    iteratorNode->next = newNode;
    list->listSize += 1;
    return 0;
}

List *create() {
    List *list = malloc(sizeof(List));
    list->head = NULL;

    return list;
}

int insertNodeToEnd(List *list, EdgeProperties value) {
    return insertNode(list, value, list->listSize);
}

int deleteNode(List* list, int index) {
    if (isEmpty(list)) {
        return -1;
    }
    if (index < 0) {
        return -1;
    }
    if (index == 0) {
        Edge *nodeToDelete = list->head;
        list->head = nodeToDelete->next;
        free(nodeToDelete);
        list->listSize -= 1;
        return 0;
    }

    Edge *iteratorNode = list->head;
    int currentIndex = 0;
    while (currentIndex != index - 1) {
        if (iteratorNode->next == NULL) {
            return -1;
        }
        ++currentIndex;
        iteratorNode = iteratorNode->next;
    }
    Edge *nodeToDelete = iteratorNode->next;
    iteratorNode->next = nodeToDelete->next;
    free(nodeToDelete);
    list->listSize -= 1;
    return 0;
}

void clear(List *list) {
    while (!isEmpty(list)) {
        deleteNode(list, 0);
    }
    free(list);
}

/*Type findNode(List *list, int index, int *errorCode) {
    if (isEmpty(list) || index < 0) {
        *errorCode = -1;
        return (Type)0;
    }
    Node *iteratorNode = list->head;

    for (int i = 0; i < index; ++i) {
        iteratorNode = iteratorNode->next;
        if (iteratorNode == NULL) {
            *errorCode = -1;
            return (Type)0;
        }
    }

    *errorCode = 0;
    return iteratorNode->value;
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
}*/

