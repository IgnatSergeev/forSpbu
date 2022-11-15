#include "customList.h"
#include <stdio.h>

typedef struct Node {
    Type value;
    struct Node *next;
} Node;

struct List {
    Node *head;
};

/*void print(List *list) {
    Node *temp = list->head;
    while (temp->next != NULL) {
        printf("%d ", temp->value);
        temp = temp->next;
    }
    printf("%d\n", temp->value);
}*/

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
    return 0;
}

List *create() {
    List *list = malloc(sizeof(List));
    list->head = NULL;

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
    return 0;
}

Type findNode(List *list, int index, int *errorCode) {
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

Node *mergeNodeSort(Node *begin, Node *end, int startIndex, int endIndex, int (*compare)(Type, Type)) {
    if (begin == end) {
        return begin;
    }

    int middleIndex = (startIndex + endIndex)/2;
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
    Node *firstPartBegin = mergeNodeSort(begin, leftMiddleNode, startIndex, currentIndex);
    Node *secondPartBegin = mergeNodeSort(nodeAfterLeftMiddleNode, end, currentIndex + 1, endIndex);

    int firstPartIndex = startIndex;
    Node *firstPartNode = firstPartBegin;
    int secondPartIndex = currentIndex + 1;
    Node *secondPartNode = secondPartBegin;

    Node *startNode = NULL;
    if (firstPartNode->value > secondPartNode->value) {
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

        if (secondPartNode->value < firstPartNode->value) {
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

