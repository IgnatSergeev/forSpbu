#include "customListForHashMap.h"
#include <stdio.h>
#include <string.h>

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
        Node *newNode = calloc(1, sizeof(Node));
        if (newNode == NULL) {
            return -1;
        }
        newNode->next = list->head;
        strcpy(newNode->value.key, value.key);
        newNode->value.value = value.value;
        list->head = newNode;
        list->listSize += 1;
        return 0;
    }
    if (isListEmpty(list)) {
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
    strcpy(newNode->value.key, value.key);
    newNode->value.value = value.value;
    newNode->next = iteratorNode->next;
    iteratorNode->next = newNode;
    list->listSize += 1;
    return 0;
}

int insertNodeToEnd(List *list, Type value) {
    return insertNode(list, value, list->listSize);
}

List *create() {
    List *list = malloc(sizeof(List));
    if (list == NULL) {
        return NULL;
    }
    list->head = NULL;
    list->listSize = 0;

    return list;
}

void printList(List *list) {
    if (isListEmpty(list)) {
        return;
    }
    Node *iteratorNode = list->head;
    while (iteratorNode != NULL) {
        printf("%s : %d\n", iteratorNode->value.key, iteratorNode->value.value);
        iteratorNode = iteratorNode->next;
    }
}

Type deleteNode(List* list, int index) {
    Type nullResult = {0};
    if (isListEmpty(list)) {
        return nullResult;
    }
    if (index < 0) {
        return nullResult;
    }
    if (index == 0) {
        Node *nodeToDelete = list->head;
        list->head = nodeToDelete->next;
        Type value = nodeToDelete->value;
        free(nodeToDelete);
        list->listSize -= 1;
        return value;//user must free value.key after all
    }

    Node *iteratorNode = list->head;
    int currentIndex = 0;
    while (currentIndex != index - 1) {
        if (iteratorNode->next == NULL) {
            return nullResult;
        }
        ++currentIndex;
        iteratorNode = iteratorNode->next;
    }
    Node *nodeToDelete = iteratorNode->next;
    iteratorNode->next = nodeToDelete->next;
    Type value = nodeToDelete->value;
    free(nodeToDelete);
    list->listSize -= 1;
    return value;
}

Type deleteLastNode(List *list) {
    return deleteNode(list, list->listSize - 1);
}

Type findNode(List *list, int index, int *errorCode) {
    Type nullResult = {0};
    if (isListEmpty(list) || index < 0) {
        *errorCode = -1;
        return nullResult;
    }
    Node *iteratorNode = list->head;

    for (int i = 0; i < index; ++i) {
        iteratorNode = iteratorNode->next;
        if (iteratorNode == NULL) {
            *errorCode = -1;
            return nullResult;
        }
    }

    *errorCode = 0;
    return iteratorNode->value;
}

int findNodeIndexByValue(List *list, Type value) {
    if (isListEmpty(list)) {
        return -1;
    }
    Node *iteratorNode = list->head;

    int currentIndex = 0;
    while (iteratorNode != NULL) {
        if (!strcmp(iteratorNode->value.key, value.key)) {
            break;
        }
        ++currentIndex;
        iteratorNode = iteratorNode->next;
    }

    if (iteratorNode == NULL) {
        return -1;
    }
    return currentIndex;
}

bool isListEmpty(List *list) {
    return list->head == NULL;
}

void clear(List *list) {
    while (!isListEmpty(list)) {
        deleteNode(list, 0);
    }
    free(list);
}

int changeNode(List *list, int index, Type value) {
    if (index < 0) {
        return -1;
    }
    if (isListEmpty(list)) {
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

    strcpy(iteratorNode->value.key, value.key);
    iteratorNode->value.value = value.value;
    return 0;
}

int changeNodeValueByOne(List *list, int index) {
    if (index < 0) {
        return -1;
    }
    if (isListEmpty(list)) {
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

    ++iteratorNode->value.value;
    return 0;
}
