B = 2000*ones(88,108);
Noise = fix(100*rand(88,108));
B = B+Noise;
figure,mesh(B);
axis([0 108 0 88 0 4000]);
xlswrite('TouchData',B);

Noise = fix(100*rand(88,108))+800;
B = B+Noise;
figure,mesh(B);
axis([0 108 0 88 0 4000]);
xlswrite('NoTouchData',B);