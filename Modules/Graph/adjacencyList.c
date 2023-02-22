#include "typeDef.h"
#include <malloc.h>
#include <stdbool.h>

typedef struct Edge {
    EdgeProperties value;
    struct Edge *next;
} Edge;

typedef struct AdjacencyList {
    Edge *head;
    int listSize;
    NodeData nodeData;
} AdjacencyList;

bool isAdjacencyListEmpty(AdjacencyList *list) {
    return list->head == NULL;
}

int insertNodeIntoAdjacencyList(AdjacencyList *list, EdgeProperties value, int index) {
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
    if (isAdjacencyListEmpty(list)) {
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

AdjacencyList *createAdjacencyList() {
    AdjacencyList *list = malloc(sizeof(AdjacencyList));
    list->head = NULL;
    list->listSize = 0;

    return list;
}

int insertNodeToEndIntoAdjacencyList(AdjacencyList *list, EdgeProperties value) {
    return insertNodeIntoAdjacencyList(list, value, list->listSize);
}

int deleteNodeFromAdjacencyList(AdjacencyList* list, int index) {
    if (isAdjacencyListEmpty(list)) {
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

void clearAdjacencyList(AdjacencyList *list) {
    while (!isAdjacencyListEmpty(list)) {
        deleteNodeFromAdjacencyList(list, 0);
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

