#include "graph.h"
#include "malloc.h"
#include "typeDef.h"
#include "adjacencyList.c"
#include "nodesDataList.c"

typedef struct Graph {
    int graphSize;
    AdjacencyList **adjacencyLists;
} Graph;

Graph *createGraph(int numberOfNodes, NodeData nodesData[]) {
    Graph *graph = calloc(1, sizeof(Graph));
    if (graph == NULL) {
        return NULL;
    }
    graph->graphSize = numberOfNodes;
    graph->adjacencyLists = calloc(numberOfNodes, sizeof(AdjacencyList *));
    if (graph->adjacencyLists == NULL) {
        free(graph);
        return NULL;
    }
    for (int i = 0; i < numberOfNodes; i++) {
        graph->adjacencyLists[i] = createAdjacencyList();
        graph->adjacencyLists[i]->nodeData = nodesData[i];
        if (graph->adjacencyLists[i] == NULL) {
            for (int j = 0; j < i; j++) {
                clearAdjacencyList(graph->adjacencyLists[j]);
            }
            free(graph->adjacencyLists);
            free(graph);
            return NULL;
        }
    }

    return graph;
}

void clearGraph(Graph *graph) {
    for (int j = 0; j < graph->graphSize; j++) {
        clearAdjacencyList(graph->adjacencyLists[j]);
    }
    free(graph->adjacencyLists);
    free(graph);
}

int addEdge(Graph *graph, int indexOfStartNode, EdgeProperties edgeProperties) {
    AdjacencyList *listOfConnectedNodes = graph->adjacencyLists[indexOfStartNode];
    return insertNodeToEndIntoAdjacencyList(listOfConnectedNodes, edgeProperties);
}

void depthFirstSearch(Graph *graph, NodeData (*whatToDoWithTheValue)(NodeData), int currentNodeIndex) {
    AdjacencyList *listOfEdges = graph->adjacencyLists[currentNodeIndex];
    Edge *iteratorNode = listOfEdges->head;
    while (iteratorNode != NULL) {
        depthFirstSearch(graph, whatToDoWithTheValue, iteratorNode->value.endNodeIndex);
        iteratorNode = iteratorNode->next;
    }
    listOfEdges->nodeData = (*whatToDoWithTheValue)(listOfEdges->nodeData);
}

Node *findClosestToCapitalNode(NodesDataList *list) {//даётся список со всеми крайними нодами принадлежащими стране
    if (isEmpty(list)) {
        return NULL;
    }
    int minDistance = -1;
    Node *closestNode = NULL;
    Node *iteratorNode = list->head;
    while (iteratorNode != NULL) {
        if (minDistance == -1 || iteratorNode->value.distanceToTheCapital < minDistance) {
            minDistance = iteratorNode->value.distanceToTheCapital;
            closestNode = iteratorNode;
        }
        iteratorNode = iteratorNode->next;
    }

    return closestNode;
}

void addNodeToTheCountry(Graph *graph, NodesDataList *list) {
    Node *closestToCapitalNode = findClosestToCapitalNode(list);
    int countryIndex = closestToCapitalNode->value.countryIndex;
    int startDistance = closestToCapitalNode->value.distanceToTheCapital;
    AdjacencyList *listWithNeighbours = graph->adjacencyLists[closestToCapitalNode->value.index];
    Edge *iteratorNode = listWithNeighbours->head
}