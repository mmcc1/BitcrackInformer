# BitcrackInformer

BitCrack Informer is a companion tool to [BitCrack](https://github.com/brichard19/BitCrack).  This program employs Deep Learning to discover candidate private key keyspaces which can then be used by Bitcrack.  
  
BitCrack Informer is a set of two programs:  

BitcrackByteInformer - Discovers weak bytes   
BitcrackCandidateInformer - Produces candidate weak keyspaces  

Requirements:  
SQL Server  
Visual Studio 2022  
Bitcrack (Nvidia/CUDA release)
  
To Use:  
Run BitcrackByteInformer to generate weak bytes which are saved to SQL.  
Run BitcrackCandidateInformer to take those bytes and produce candidate keyspaces  

Setup:  
Open in Visual Studio  
Navigate to BTCDBContext.cs (found in LibDAL project) and add your SQL connection string.  
Use package manager console to add-migration and update-database to push the schema to the DB  
Run BitcrackByteInformer for a few hours to populate the DB  
Add your public addresses to the Init() method of EngineCandidateA  
Run BitCrackCandidateInformer and wait for it to print the candidates to the screen  
Copy the candidate keyspaces and store them in a text file  
Use the keyspaces with BitCrack  

Re-run BitcrackByteInformer to add to the DB and BitCrackCandidateInformer to generate new keyspaces.  If you are seeing the same results, purge the DB.

Example Bitcrack command:  
cuBitCrack --keyspace b9cfdad58d43048e34aa97a3ec08230ab40bc87811f6f973b67a050000000000:b9cfdad58d43048e34aa97a3ec08230ab40bc87811f6f973b67a05FFFFFFFFFF -i addresses.txt -o privatekeys -b 256 -t 256 -p256
