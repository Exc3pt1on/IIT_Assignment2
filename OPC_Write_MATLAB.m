clear, clc

%Connect to server
da = opcda('localhost', 'Matrikon.OPC.Simulation.1');
connect(da);

%Create group
grp = addgroup(da, 'Group1');

%Add tags
ItemList = {'Bucket Brigade.Real4'};
item = additem(grp, ItemList);

while true
    value = input("OPC_Write: Temperatur i Celsius: ");
    write(grp, value);
end

