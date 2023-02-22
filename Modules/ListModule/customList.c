#include "customList.h"
#include <stdio.h>

#include <string.h>

typedef struct Node {
    Type value;
    int frequency;

    struct Node *next;
} Node;

struct List {
    Node *head;
    int listSize;
};

void print(List *list, void (*print)(Type)) {
    Node *temp = list->head;
    while (temp != NULL) {
        (*print)(temp->value);
        temp = temp->next;
    }
}


int insertNode(List *list, Type value, int keySize, int index) {
    if (index < 0) {
        return -1;
    }
    if (index == 0) {
        Node *newNode = calloc(1, sizeof(Node));
        if (newNode == NULL) {
            return -1;
        }
        newNode->next = list->head;

        newNode->value = calloc(keySize, sizeof(char));
        strcpy(newNode->value, value);
        newNode->frequency = 1;
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

    strcpy(newNode->value, value);
    newNode->next = iteratorNode->next;
    iteratorNode->next = newNode;
    list->listSize += 1;
    return 0;
}

int insertNodeToEnd(List *list, Type value, int keySize) {
    return insertNode(list, value, keySize, list->listSize);
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

int deleteNode(List* list, int index) {
    if (isEmpty(list)) {
        return -1;
    }
    if (index < 0) {
        return -1;
    }
    if (index == 0) {
        Node *nodeToDelete = list->head;
        list->head = nodeToDelete->next;
        free(nodeToDelete);
        list->listSize -= 1;

        return 0;
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
    Node *nodeToDelete = iteratorNode->next;
    iteratorNode->next = nodeToDelete->next;
    free(nodeToDelete);


    list->listSize -= 1;

    return 0;
}

Type findNode(List *list, int index, Type zeroValue, int *errorCode) {
    if (isEmpty(list) || index < 0) {
        *errorCode = -1;
        return zeroValue;
    }
    Node *iteratorNode = list->head;

    for (int i = 0; i < index; ++i) {
        iteratorNode = iteratorNode->next;
        if (iteratorNode == NULL) {
            *errorCode = -1;
            return zeroValue;
        }
    }

    *errorCode = 0;
    return iteratorNode->value;
}



int findNodeIndexByValue(List *list, Type value) {
    if (isEmpty(list)) {
        return -1;
    }
    Node *iteratorNode = list->head;

    int currentIndex = 0;
    while (iteratorNode != NULL) {
        if (!strcmp(iteratorNode->value, value)) {
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


    strcpy(iteratorNode->value, value);
    ++iteratorNode->frequency;

    return 0;
}

Node *mergeNodeSort(Node *begin, Node *end, int startIndex, int endIndex, int (*compare)(Type, Type)) {
    if (begin == end) {
        return begin;
    }

    int middleIndex = (startIndex + endIndex) / 2;
    int currentIndex = startIndex;
    Node *leftMiddleNode = begin;
    while (currentIndex < middleIndex) {
        ++currentIndex;
        leftMiddleNode = leftMiddleNode->next;
    }

    Node *nodeAfterEnd = end->next;
    end->next = NULL;
    Node *nodeAfterLeftMiddleNode = leftMiddleNode->next;
    leftMiddleNode->next = NULL;
    Node *firstPartBegin = mergeNodeSort(begin, leftMiddleNode, startIndex, currentIndex, compare);
    Node *secondPartBegin = mergeNodeSort(nodeAfterLeftMiddleNode, end, currentIndex + 1, endIndex, compare);

    int firstPartIndex = startIndex;
    Node *firstPartNode = firstPartBegin;
    int secondPartIndex = currentIndex + 1;
    Node *secondPartNode = secondPartBegin;

    Node *startNode = NULL;
    if ((*compare)(firstPartNode->value, secondPartNode->value) == 1) {
        startNode = secondPartNode;
        secondPartNode = secondPartNode->next;
        ++secondPartIndex;
    } else {
        startNode = firstPartNode;
        firstPartNode = firstPartNode->next;
        ++firstPartIndex;
    }

    Node *lastNode = startNode;
    while (firstPartIndex != (currentIndex + 1) || secondPartIndex != (endIndex + 1)) {
        if (firstPartIndex == (currentIndex + 1)) {
            lastNode->next = secondPartNode;
            secondPartNode = secondPartNode->next;
            ++secondPartIndex;
            lastNode = lastNode->next;
            continue;
        }
        if (secondPartIndex == (endIndex + 1)) {
            lastNode->next = firstPartNode;
            firstPartNode = firstPartNode->next;
            ++firstPartIndex;
            lastNode = lastNode->next;
            continue;
        }

        if ((*compare)(firstPartNode->value, secondPartNode->value) == -1) {
            lastNode->next = secondPartNode;
            secondPartNode = secondPartNode->next;
            ++secondPartIndex;
            lastNode = lastNode->next;
        } else {
            lastNode->next = firstPartNode;
            firstPartNode = firstPartNode->next;
            ++firstPartIndex;
            lastNode = lastNode->next;
        }
    }
    lastNode->next = nodeAfterEnd;
    return startNode;
}

int mergeSort(List *list, int (*compare)(Type, Type)) {
    if (isEmpty(list)) {
        return -1;
    }
    if (list->head->next == NULL) {
        return 0;
    }
    Node *beginNode = list->head;
    Node *endNode = list->head;
    int currentIndex = 0;
    while (endNode->next != NULL) {
        ++currentIndex;
        endNode = endNode->next;
    }

    list->head = mergeNodeSort(beginNode, endNode, 0, currentIndex, compare);

    return 0;
}

