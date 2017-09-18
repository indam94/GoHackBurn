#include<iostream>
#define MAX_HASH 7
#define HASH_KEY(key) key%MAX_HASH

using namespace std;

typedef struct Node
{
    int id;
    Node* hashNext;
}Node;

Node* hashTable[MAX_HASH];

void  AddHashData(int key, Node* node)
{
    int hash_key = HASH_KEY(key);
    if (hashTable[hash_key] == NULL)
    {
        hashTable[hash_key] = node;
    }
    else
    {
        node->hashNext = hashTable[hash_key];
        hashTable[hash_key] = node;
    }
}

void DelHashData(int id)
{
    int hash_key = HASH_KEY(id);
    if (hashTable[hash_key] == NULL)
        return;

    Node* delNode = NULL;
    if (hashTable[hash_key]->id == id)
    {
        delNode = hashTable[hash_key];
        hashTable[hash_key] = hashTable[hash_key]->hashNext;
    }
    else
    {
        Node* node = hashTable[hash_key];
        Node* next = node->hashNext;
        while (next)
        {
            if (next->id == id)
            {
                node->hashNext = next->hashNext;
                delNode = next;
                break;
            }
            node = next;
            next = node->hashNext;
        }
    }
    free(delNode);
}

Node* FindHashData(int id)
{
    int hash_key = HASH_KEY(id);
    if (hashTable[hash_key] == NULL)
        return NULL;

    if (hashTable[hash_key]->id == id)
    {
        return hashTable[hash_key];
    }
    else
    {
        Node* node = hashTable[hash_key];
        while (node->hashNext)
        {
            if (node->hashNext->id == id)
            {
                return node->hashNext;
            }
            node = node->hashNext;
        }
    }
    return  NULL;
}

void PrintAllHashData()
{
  cout<<"(";
    for (int i = MAX_HASH; i > -1; i--)
    {
        //cout << " idx"<<i<<":"/*<< endl*/;
        if (hashTable[i] != NULL)
        {
            Node* node = hashTable[i];
            cout << " "<<node->id << " ";
            while (node->hashNext)
            {
              //cout <<" "<< node->id << " ";
              node = node->hashNext;
              cout <<" "<< node->id << " ";

            }
            //cout << " "<<node->id << " ";
        }
    }
    cout<<")";
    cout << endl;
}

int main()
{
  int num;
  int ntn;
  char input[15];

  while(1){
    cout<<"(1.find, 2. insert, 3. delete, 4. print 5. quit)"<<endl;
    cin>>input;
    Node* node = (Node*)malloc(sizeof(Node));
    if(strcmp(input,"find")==0){
      cout<<"Enter a data to find=>";
      cin>>num;
      if(FindHashData(num)==NULL)
        cout<<"Not Found"<<endl;
      else
        cout<<num<<" Found"<<endl;
      PrintAllHashData();
        
    }
    else if(strcmp(input,"insert")==0){
      cout<<"Enter a data to insert=>";
      cin>>num;
      node->id = num;
      node->hashNext = NULL;
      AddHashData(node->id, node);
      PrintAllHashData();
    }
    else if(strcmp(input,"delete")==0){
      cout<<"Enter a data to delete=>";
      cin>>num;
      DelHashData(num);
      PrintAllHashData();
    }
    else if(strcmp(input,"print")==0){
      PrintAllHashData();
    }
    else if(strcmp(input,"quit")==0){
      exit(0);
    }
    else
      cout<<"Bad Command!\n";
  }

    return 0;
}
