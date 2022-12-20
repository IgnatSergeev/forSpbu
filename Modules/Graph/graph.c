#include "graph.h"
#include "malloc.h"
#include "typeDef.h"
#include "queueForGraph.h"

struct Graph {
    int graphSize;
    NodeData *nodesDataArray;
    int **adjacencyMatrix;
};

Graph *createGraph(int numberOfNodes, NodeData nodesData[]) {
    Graph *graph = calloc(1, sizeof(Graph));
    if (graph == NULL) {
        return NULL;
    }
    graph->nodesDataArray = calloc(numberOfNodes, sizeof(NodeData));
    if (graph->nodesDataArray == NULL) {
        free(graph);
        return NULL;
    }
    for (int i = 0; i < numberOfNodes; i++) {
        graph->nodesDataArray[i] = nodesData[i];
    }
    graph->graphSize = numberOfNodes;
    graph->adjacencyMatrix = calloc(numberOfNodes, sizeof(int *));
    if (graph->adjacencyMatrix == NULL) {
        free(graph->nodesDataArray);
        free(graph);
        return NULL;
    }
    for (int i = 0; i < numberOfNodes; i++) {
        graph->adjacencyMatrix[i] = calloc(numberOfNodes, sizeof(int));
        for (int j = 0; j < numberOfNodes; j++) {
            if (i != j) {
                graph->adjacencyMatrix[i][j] = -1;
            }
        }
    }

    return graph;
}

void clearGraph(Graph *graph) {
    for (int j = 0; j < graph->graphSize; j++) {
        free(graph->adjacencyMatrix[j]);
    }
    free(graph->adjacencyMatrix);
    free(graph->nodesDataArray);
    free(graph);
}

void addEdge(Graph *graph, int indexOfStartNode, int indexOfEndNode, int length) {
    graph->adjacencyMatrix[indexOfStartNode][indexOfEndNode] = length;
    graph->adjacencyMatrix[indexOfEndNode][indexOfStartNode] = length;
}

void changeNodeData(Graph *graph, int index, NodeData nodeData) {
    graph->nodesDataArray[index] = nodeData;
}

void depthFirstSearch(Graph *graph, NodeData (*whatToDoWithTheValue)(NodeData), int currentNodeIndex, bool *isVisited) {
    for (int i = 0; i < graph->graphSize; i++) {
        if (currentNodeIndex != i && graph->adjacencyMatrix[currentNodeIndex][i] != -1 && !isVisited[i]) {
            isVisited[i] = true;
            depthFirstSearch(graph, whatToDoWithTheValue, i, isVisited);
        }
    }
    graph->nodesDataArray[currentNodeIndex] = (*whatToDoWithTheValue)(graph->nodesDataArray[currentNodeIndex]);
}

void breadthFirstSearch(Graph *graph, NodeData (*whatToDoWithTheValue)(NodeData), int nodeIndex) {
    bool *isVisited = calloc(graph->graphSize, sizeof(bool));
    Queue *queue = createQueue();
    enqueue(queue, nodeIndex);
    isVisited[nodeIndex] = true;
    while (!isEmpty(queue)) {
        int currentNodeIndex = dequeue(queue, NULL);
        for (int i = 0; i < graph->graphSize; i++) {
            if (currentNodeIndex != i && graph->adjacencyMatrix[currentNodeIndex][i] != -1 && !isVisited[i]) {
                isVisited[i] = true;
                enqueue(queue, i);
            }
        }
    }
    free(isVisited);
}

int *minimalDistancesFromNodeIndexToEachNode(Graph *graph, int nodeIndex) {
    bool *isVisited = calloc(graph->graphSize, sizeof(bool));
    int *minimalDistancesToTheIndexNode = calloc(graph->graphSize, sizeof(int));
    for (int i = 0; i < graph->graphSize; i++) {
        minimalDistancesToTheIndexNode[i] = INT_MAX;
    }
    minimalDistancesToTheIndexNode[nodeIndex] = 0;
    for (int i = 0; i < graph->graphSize; i++) {
        int closestNode = -1;
        for (int j = 0; j < graph->graphSize; j++) {
            if (!isVisited[j] && (closestNode == -1 || minimalDistancesToTheIndexNode[j] < minimalDistancesToTheIndexNode[i])) {
                closestNode = j;
            }
        }
        if (minimalDistancesToTheIndexNode[closestNode] == INT_MAX) {
            free(isVisited);
            return minimalDistancesToTheIndexNode;
        }
        isVisited[closestNode] = true;
        for (int j = 0; j < graph->graphSize; j++) {
            if (graph->adjacencyMatrix[closestNode][j] != -1 && closestNode != j && (minimalDistancesToTheIndexNode[closestNode] + graph->adjacencyMatrix[closestNode][j] < minimalDistancesToTheIndexNode[j])) {
                minimalDistancesToTheIndexNode[j] = minimalDistancesToTheIndexNode[closestNode] + graph->adjacencyMatrix[closestNode][j];
            }
        }
    }
    free(isVisited);
    return minimalDistancesToTheIndexNode;
}

int **minimalDistancesFromEachToEachNode(Graph *graph) {
    int **minimalDistances = calloc(graph->graphSize, sizeof(int *));
    for (int i = 0; i < graph->graphSize; i++) {
        minimalDistances[i] = calloc(graph->graphSize, sizeof(int));
        for (int j = 0; j < graph->graphSize; j++) {
            minimalDistances[i][j] = INT_MAX / 3;
        }
    }

    for (int i = 0; i < graph->graphSize; i++) {
        for (int j = 0; j < graph->graphSize; j++) {
            if (graph->adjacencyMatrix[i][j] != -1) {
                minimalDistances[i][j] = graph->adjacencyMatrix[i][j];
            }
        }
    }

    for (int k = 0; k < graph->graphSize; k++) {
        for (int j = 0; j < graph->graphSize; j++) {
            for (int i = 0; i < graph->graphSize; i++) {
                if (minimalDistances[i][k] + minimalDistances[k][j] < minimalDistances[i][j]) {
                    minimalDistances[i][j] = minimalDistances[i][k] + minimalDistances[k][j];
                }
            }
        }
    }

    return minimalDistances;
}
