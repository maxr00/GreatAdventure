//Maya ASCII 2019 scene
//Name: ModularMarketTent.ma
//Last modified: Fri, Oct 25, 2019 10:54:25 AM
//Codeset: 1252
requires maya "2019";
requires "mtoa" "3.3.0";
currentUnit -l centimeter -a degree -t film;
fileInfo "application" "maya";
fileInfo "product" "Maya 2019";
fileInfo "version" "2019";
fileInfo "cutIdentifier" "201812112215-434d8d9c04";
fileInfo "osv" "Microsoft Windows 10 Technical Preview  (Build 18362)\n";
fileInfo "license" "education";
createNode transform -s -n "persp";
	rename -uid "A19EAA1F-4CB8-E67A-6685-14AEF81DC740";
	setAttr ".v" no;
	setAttr ".t" -type "double3" -2.8701477015952244 4.4919243225940209 14.459853996101042 ;
	setAttr ".r" -type "double3" -16.538352729617156 359.39999999997445 -7.4548337879467452e-17 ;
createNode camera -s -n "perspShape" -p "persp";
	rename -uid "E67F8AEA-4381-FE5F-FCB1-A99537C24B17";
	setAttr -k off ".v" no;
	setAttr ".fl" 34.999999999999993;
	setAttr ".coi" 12.758229269977978;
	setAttr ".imn" -type "string" "persp";
	setAttr ".den" -type "string" "persp_depth";
	setAttr ".man" -type "string" "persp_mask";
	setAttr ".tp" -type "double3" -2.5 -0.25 3.1695864200592041 ;
	setAttr ".hc" -type "string" "viewSet -p %camera";
	setAttr ".ai_translator" -type "string" "perspective";
createNode transform -s -n "top";
	rename -uid "AA2C5C40-487A-1360-E190-3B9154428DA7";
	setAttr ".v" no;
	setAttr ".t" -type "double3" -0.22289587873639327 1000.1 0.20938703760085442 ;
	setAttr ".r" -type "double3" -90 0 0 ;
createNode camera -s -n "topShape" -p "top";
	rename -uid "F9FE40F1-4174-2201-01CC-40B990C64501";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 1000.1;
	setAttr ".ow" 10.678738917643559;
	setAttr ".imn" -type "string" "top";
	setAttr ".den" -type "string" "top_depth";
	setAttr ".man" -type "string" "top_mask";
	setAttr ".hc" -type "string" "viewSet -t %camera";
	setAttr ".o" yes;
	setAttr ".ai_translator" -type "string" "orthographic";
createNode transform -s -n "front";
	rename -uid "00626D4C-4438-1645-D8B4-88BCDF02EAB1";
	setAttr ".v" no;
	setAttr ".t" -type "double3" -0.13259934023806613 -0.57775426818014552 1000.1 ;
createNode camera -s -n "frontShape" -p "front";
	rename -uid "708EB050-44D5-7901-2026-DA862686B9A6";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 1000.1;
	setAttr ".ow" 12.690646768988554;
	setAttr ".imn" -type "string" "front";
	setAttr ".den" -type "string" "front_depth";
	setAttr ".man" -type "string" "front_mask";
	setAttr ".hc" -type "string" "viewSet -f %camera";
	setAttr ".o" yes;
	setAttr ".ai_translator" -type "string" "orthographic";
createNode transform -s -n "side";
	rename -uid "C2BE9F90-4846-BBBE-F6CD-8CA0B0697D53";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 1000.1 0 0 ;
	setAttr ".r" -type "double3" 0 90 0 ;
createNode camera -s -n "sideShape" -p "side";
	rename -uid "6AA879CB-4F5E-9A74-6C9A-5C89AE4B7B3D";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 1000.1;
	setAttr ".ow" 12.589922616550682;
	setAttr ".imn" -type "string" "side";
	setAttr ".den" -type "string" "side_depth";
	setAttr ".man" -type "string" "side_mask";
	setAttr ".hc" -type "string" "viewSet -s %camera";
	setAttr ".o" yes;
	setAttr ".ai_translator" -type "string" "orthographic";
createNode transform -n "pCube1";
	rename -uid "7185F36C-4F66-8270-925D-9FBC34BCC91D";
	setAttr ".s" -type "double3" 1 0.5 6 ;
createNode mesh -n "pCubeShape1" -p "pCube1";
	rename -uid "A54F3033-4DA4-0F8A-6241-A0AADCC627EA";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.5 0.375 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 27 ".pt";
	setAttr ".pt[0]" -type "float3" 0 0.4937225 0 ;
	setAttr ".pt[1]" -type "float3" 0 0.4937225 0 ;
	setAttr ".pt[4]" -type "float3" 0 1.5130203 0 ;
	setAttr ".pt[5]" -type "float3" 0 1.5130203 0 ;
	setAttr ".pt[6]" -type "float3" 0 2.006743 0 ;
	setAttr ".pt[7]" -type "float3" 0 2.006743 0 ;
	setAttr ".pt[12]" -type "float3" 0.25555557 0.72492582 0 ;
	setAttr ".pt[13]" -type "float3" -0.25555557 0.72492582 0 ;
	setAttr ".pt[14]" -type "float3" -0.25555557 0.72492582 0 ;
	setAttr ".pt[15]" -type "float3" 0.25555557 0.72492582 0 ;
	setAttr ".pt[16]" -type "float3" 0 0.62136489 0 ;
	setAttr ".pt[17]" -type "float3" 0 0.62136489 0 ;
	setAttr ".pt[18]" -type "float3" 0 0.62136489 0 ;
	setAttr ".pt[19]" -type "float3" 0 0.62136489 0 ;
	setAttr ".pt[20]" -type "float3" -0.083333343 0.62136489 0 ;
	setAttr ".pt[21]" -type "float3" -0.083333343 0.62136489 0 ;
	setAttr ".pt[22]" -type "float3" 0.083333343 0.62136489 0 ;
	setAttr ".pt[23]" -type "float3" 0.083333343 0.62136489 0 ;
	setAttr ".pt[24]" -type "float3" 0 0.62136489 0 ;
	setAttr ".pt[25]" -type "float3" 0 0.62136489 0 ;
	setAttr ".pt[26]" -type "float3" 0 0.62136489 0 ;
	setAttr ".pt[27]" -type "float3" 0 0.62136489 0 ;
	setAttr ".pt[31]" -type "float3" 0 1.5130203 0 ;
	setAttr ".pt[32]" -type "float3" 0 2.006743 0 ;
	setAttr ".pt[33]" -type "float3" 0 0.4937225 0 ;
	setAttr ".pt[34]" -type "float3" 0 0.62136489 0 ;
	setAttr ".pt[35]" -type "float3" 0 0.62136489 0 ;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode transform -n "pCube2";
	rename -uid "C7B1851B-478E-B53A-E216-35849750F299";
	setAttr ".t" -type "double3" 2.4699189771521279 0 0 ;
	setAttr ".s" -type "double3" 1 0.5 6 ;
createNode mesh -n "pCubeShape2" -p "pCube2";
	rename -uid "582D189D-4382-87EA-84CB-C4BE95E6D235";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.5 0.5 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode mesh -n "polySurfaceShape1" -p "pCube2";
	rename -uid "393D0D25-407C-1160-D357-38891361A385";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.5 0.5 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 18 ".uvst[0].uvsp[0:17]" -type "float2" 0.375 0 0.625 0 0.375
		 0.25 0.625 0.25 0.375 0.5 0.625 0.5 0.375 0.75 0.625 0.75 0.375 1 0.625 1 0.875 0
		 0.875 0.25 0.125 0 0.125 0.25 0.5 0.25 0.5 0.5 0.5 0.75 0.5 1;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 12 ".vt[0:11]"  -0.5 -0.0062775016 0.5 0.5 -0.0062775016 0.5
		 -0.5 0.5 0.5 0.5 0.5 0.5 -0.5 2.013020277 -0.5 0.5 2.013020277 -0.5 -0.5 1.50674295 -0.5
		 0.5 1.50674295 -0.5 0 0.5 0.5 0 2.013020277 -0.5 0 1.50674295 -0.5 0 -0.0062775016 0.5;
	setAttr -s 19 ".ed[0:18]"  0 11 0 2 8 0 4 9 0 6 10 0 0 2 0 1 3 0 2 4 0
		 3 5 0 4 6 0 5 7 0 6 0 0 7 1 0 8 3 0 9 5 0 8 9 1 10 7 0 9 10 1 11 1 0 10 11 1;
	setAttr -s 8 -ch 32 ".fc[0:7]" -type "polyFaces" 
		f 4 1 14 -3 -7
		mu 0 4 2 14 15 4
		f 4 2 16 -4 -9
		mu 0 4 4 15 16 6
		f 4 3 18 -1 -11
		mu 0 4 6 16 17 8
		f 4 -12 -10 -8 -6
		mu 0 4 1 10 11 3
		f 4 10 4 6 8
		mu 0 4 12 0 2 13
		f 4 -15 12 7 -14
		mu 0 4 15 14 3 5
		f 4 -17 13 9 -16
		mu 0 4 16 15 5 7
		f 4 -19 15 11 -18
		mu 0 4 17 16 7 9;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".pd[0]" -type "dataPolyComponent" Index_Data UV 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode transform -n "pCube3";
	rename -uid "0A7E0E6C-4427-5738-673D-2096D2ED1790";
	setAttr ".t" -type "double3" 1.1660250213354539 0 0 ;
	setAttr ".s" -type "double3" 1 0.5 6 ;
createNode mesh -n "pCubeShape3" -p "pCube3";
	rename -uid "4801A6B8-4DD3-5AC7-A510-4D81C33A788F";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.5 0.125 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 6 ".pt";
	setAttr ".pt[26]" -type "float3" -0.032713424 -0.016573753 0 ;
	setAttr ".pt[27]" -type "float3" -0.032713424 -0.016573753 0 ;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode mesh -n "polySurfaceShape2" -p "pCube3";
	rename -uid "557D2122-44B2-B4F9-B150-508EAE62F20E";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.5 0.125 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 30 ".uvst[0].uvsp[0:29]" -type "float2" 0.375 0 0.625 0 0.375
		 0.25 0.625 0.25 0.375 0 0.625 0 0.625 0.25 0.375 0.25 0.375 0 0.625 0 0.625 0 0.375
		 0 0.625 0 0.625 0 0.375 0 0.375 0 0.625 0 0.625 0 0.375 0 0.375 0 0.5 0 0.5 0 0.5
		 0 0.5 0 0.5 0 0.5 0.25 0.5 0.25 0.5 0 0.5 0 0.5 0;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 30 ".vt[0:29]"  -0.5 -0.0062775016 0.5 0.5 -0.0062775016 0.5
		 -0.5 0.5 0.5 0.5 0.5 0.5 -0.5 -0.5 0.5282644 0.5 -0.5 0.5282644 0.5 0.5 0.5282644
		 -0.5 0.5 0.5282644 -0.24444443 -1.77507424 0.5 0.24444443 -1.77507424 0.5 0.24444443 -1.77507424 0.5282644
		 -0.24444443 -1.77507424 0.5282644 0.5 -0.92030162 0.5282644 0.5 -0.92030162 0.5 -0.5 -0.92030162 0.5
		 -0.5 -0.92030162 0.5282644 0.41666666 -1.41191435 0.5282644 0.41666666 -1.41191435 0.5
		 -0.41666666 -1.41191435 0.5 -0.41666666 -1.41191435 0.5282644 0 -1.87863517 0.5 0 -1.87863517 0.5282644
		 0 -1.41191435 0.5282644 0 -0.92030162 0.5282644 0 -0.5 0.5282644 0 0.5 0.5282644
		 0 0.5 0.5 0 -0.0062775016 0.5 0 -0.92030162 0.5 0 -1.41191435 0.5;
	setAttr -s 55 ".ed[0:54]"  0 27 0 2 26 0 0 2 0 1 3 0 0 4 1 1 5 1 4 24 1
		 3 6 0 5 6 0 2 7 0 7 25 0 4 7 0 0 14 0 1 13 0 8 20 0 5 12 0 9 10 0 4 15 0 11 21 0
		 8 11 0 12 16 0 13 17 0 12 13 1 14 18 0 13 28 1 15 19 0 14 15 1 15 23 1 16 10 0 17 9 0
		 16 17 1 18 8 0 17 29 1 19 11 0 18 19 1 19 22 1 20 9 0 21 10 0 20 21 1 22 16 1 21 22 1
		 23 12 1 22 23 1 24 5 1 23 24 1 25 6 0 24 25 1 26 3 0 25 26 1 27 1 0 28 14 1 27 28 1
		 29 18 1 28 29 1 29 20 1;
	setAttr -s 26 -ch 104 ".fc[0:25]" -type "polyFaces" 
		f 4 6 46 -11 -12
		mu 0 4 4 24 25 7
		f 4 14 38 -19 -20
		mu 0 4 8 20 21 11
		f 4 3 7 -9 -6
		mu 0 4 1 3 6 5
		f 4 -2 9 10 48
		mu 0 4 26 2 7 25
		f 4 -3 4 11 -10
		mu 0 4 2 0 4 7
		f 4 0 51 50 -13
		mu 0 4 0 27 28 14
		f 4 5 15 22 -14
		mu 0 4 1 5 12 13
		f 4 -7 17 27 44
		mu 0 4 24 4 15 23
		f 4 -5 12 26 -18
		mu 0 4 4 0 14 15
		f 4 -23 20 30 -22
		mu 0 4 13 12 16 17
		f 4 -51 53 52 -24
		mu 0 4 14 28 29 18
		f 4 -27 23 34 -26
		mu 0 4 15 14 18 19
		f 4 -28 25 35 42
		mu 0 4 23 15 19 22
		f 4 -31 28 -17 -30
		mu 0 4 17 16 10 9
		f 4 -53 54 -15 -32
		mu 0 4 18 29 20 8
		f 4 -35 31 19 -34
		mu 0 4 19 18 8 11
		f 4 -36 33 18 40
		mu 0 4 22 19 11 21
		f 4 36 16 -38 -39
		mu 0 4 20 9 10 21
		f 4 -40 -41 37 -29
		mu 0 4 16 22 21 10
		f 4 -42 -43 39 -21
		mu 0 4 12 23 22 16
		f 4 -44 -45 41 -16
		mu 0 4 5 24 23 12
		f 4 -47 43 8 -46
		mu 0 4 25 24 5 6
		f 4 -48 -49 45 -8
		mu 0 4 3 26 25 6
		f 4 -52 49 13 24
		mu 0 4 28 27 1 13
		f 4 -54 -25 21 32
		mu 0 4 29 28 13 17
		f 4 -55 -33 29 -37
		mu 0 4 20 29 17 9;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".pd[0]" -type "dataPolyComponent" Index_Data UV 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode transform -n "pCylinder1";
	rename -uid "19060FAD-433D-E05C-4AC4-84AB416E91A9";
	setAttr ".t" -type "double3" 1 0 0 ;
createNode mesh -n "pCylinderShape1" -p "pCylinder1";
	rename -uid "D92796AE-42B5-EA7B-B8FF-138074009321";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.49999998509883881 0.49999996274709702 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 42 ".pt[0:41]" -type "float3"  -0.8321327 0 0.27037609 -0.70785433 
		0 0.51428598 -0.51428622 0 0.70785409 -0.27037632 0 0.83213228 -1.0430281e-07 0 0.87495565 
		0.27037612 0 0.83213222 0.51428598 0 0.70785385 0.70785385 0 0.51428586 0.8321321 
		0 0.27037597 0.87495553 0 -1.5645419e-07 0.8321321 0 -0.27037629 0.70785385 0 -0.51428604 
		0.51428586 0 -0.70785409 0.27037603 0 -0.83213228 -7.8227096e-08 0 -0.87495565 -0.27037615 
		0 -0.83213222 -0.51428598 0 -0.70785397 -0.70785385 0 -0.51428604 -0.8321321 0 -0.27037624 
		-0.87495553 0 -1.5645419e-07 -0.8321327 0 0.27037609 -0.70785433 0 0.51428598 -0.51428622 
		0 0.70785409 -0.27037632 0 0.83213228 -1.0430281e-07 0 0.87495565 0.27037612 0 0.83213222 
		0.51428598 0 0.70785385 0.70785385 0 0.51428586 0.8321321 0 0.27037597 0.87495553 
		0 -1.5645419e-07 0.8321321 0 -0.27037629 0.70785385 0 -0.51428604 0.51428586 0 -0.70785409 
		0.27037603 0 -0.83213228 -7.8227096e-08 0 -0.87495565 -0.27037615 0 -0.83213222 -0.51428598 
		0 -0.70785397 -0.70785385 0 -0.51428604 -0.8321321 0 -0.27037624 -0.87495553 0 -1.5645419e-07 
		-1.0430281e-07 0 -1.5645419e-07 -1.0430281e-07 0 -1.5645419e-07;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode transform -n "pCube4";
	rename -uid "F4AA3755-446A-E0C3-C199-E6BE053643AA";
	setAttr ".t" -type "double3" -2 0 0 ;
	setAttr ".s" -type "double3" 1 0.5 6 ;
createNode transform -n "transform2" -p "pCube4";
	rename -uid "67F0ECB2-440A-0E67-CF55-6EA81189881F";
	setAttr ".v" no;
createNode mesh -n "pCubeShape4" -p "transform2";
	rename -uid "AAC4EC35-4DC0-DE28-48CE-71972FCDA618";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr ".iog[0].og[0].gcl" -type "componentList" 1 "f[0:33]";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.5 0.375 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 43 ".uvst[0].uvsp[0:42]" -type "float2" 0.375 0 0.625 0 0.375
		 0.25 0.625 0.25 0.375 0.5 0.625 0.5 0.375 0.75 0.625 0.75 0.375 1 0.625 1 0.875 0
		 0.875 0.25 0.125 0 0.125 0.25 0.375 0 0.625 0 0.625 0.25 0.375 0.25 0.375 0 0.625
		 0 0.625 0 0.375 0 0.625 0 0.625 0 0.375 0 0.375 0 0.625 0 0.625 0 0.375 0 0.375 0
		 0.5 0 0.5 0 0.5 0 0.5 0 0.5 0 0.5 0.25 0.5 0.25 0.5 0.5 0.5 0.75 0.5 0 0.5 1 0.5
		 0 0.5 0;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 27 ".pt";
	setAttr ".pt[0]" -type "float3" 0 0.4937225 0 ;
	setAttr ".pt[1]" -type "float3" 0 0.4937225 0 ;
	setAttr ".pt[4]" -type "float3" 0 1.5130203 0 ;
	setAttr ".pt[5]" -type "float3" 0 1.5130203 0 ;
	setAttr ".pt[6]" -type "float3" 0 2.006743 0 ;
	setAttr ".pt[7]" -type "float3" 0 2.006743 0 ;
	setAttr ".pt[12]" -type "float3" 0.25555557 0.72492582 0 ;
	setAttr ".pt[13]" -type "float3" -0.25555557 0.72492582 0 ;
	setAttr ".pt[14]" -type "float3" -0.25555557 0.72492582 0 ;
	setAttr ".pt[15]" -type "float3" 0.25555557 0.72492582 0 ;
	setAttr ".pt[16]" -type "float3" 0 0.62136489 0 ;
	setAttr ".pt[17]" -type "float3" 0 0.62136489 0 ;
	setAttr ".pt[18]" -type "float3" 0 0.62136489 0 ;
	setAttr ".pt[19]" -type "float3" 0 0.62136489 0 ;
	setAttr ".pt[20]" -type "float3" -0.083333343 0.62136489 0 ;
	setAttr ".pt[21]" -type "float3" -0.083333343 0.62136489 0 ;
	setAttr ".pt[22]" -type "float3" 0.083333343 0.62136489 0 ;
	setAttr ".pt[23]" -type "float3" 0.083333343 0.62136489 0 ;
	setAttr ".pt[24]" -type "float3" 0 0.62136489 0 ;
	setAttr ".pt[25]" -type "float3" 0 0.62136489 0 ;
	setAttr ".pt[26]" -type "float3" 0 0.62136489 0 ;
	setAttr ".pt[27]" -type "float3" 0 0.62136489 0 ;
	setAttr ".pt[31]" -type "float3" 0 1.5130203 0 ;
	setAttr ".pt[32]" -type "float3" 0 2.006743 0 ;
	setAttr ".pt[33]" -type "float3" 0 0.4937225 0 ;
	setAttr ".pt[34]" -type "float3" 0 0.62136489 0 ;
	setAttr ".pt[35]" -type "float3" 0 0.62136489 0 ;
	setAttr -s 36 ".vt[0:35]"  -0.5 -0.5 0.5 0.5 -0.5 0.5 -0.5 0.5 0.5 0.5 0.5 0.5
		 -0.5 0.5 -0.5 0.5 0.5 -0.5 -0.5 -0.5 -0.5 0.5 -0.5 -0.5 -0.5 -0.5 0.5282644 0.5 -0.5 0.5282644
		 0.5 0.5 0.5282644 -0.5 0.5 0.5282644 -0.5 -2.5 0.5 0.5 -2.5 0.5 0.5 -2.5 0.5282644
		 -0.5 -2.5 0.5282644 0.5 -1.54166651 0.5282644 0.5 -1.54166651 0.5 -0.5 -1.54166651 0.5
		 -0.5 -1.54166651 0.5282644 0.5 -2.033279181 0.5282644 0.5 -2.033279181 0.5 -0.5 -2.033279181 0.5
		 -0.5 -2.033279181 0.5282644 0 -2.5 0.5 0 -2.5 0.5282644 0 -2.033279181 0.5282644
		 0 -1.54166651 0.5282644 0 -0.5 0.5282644 0 0.5 0.5282644 0 0.5 0.5 0 0.5 -0.5 0 -0.5 -0.5
		 0 -0.5 0.5 0 -1.54166651 0.5 0 -2.033279181 0.5;
	setAttr -s 68 ".ed[0:67]"  0 33 0 2 30 0 4 31 0 6 32 0 0 2 0 1 3 0 2 4 0
		 3 5 0 4 6 0 5 7 0 6 0 0 7 1 0 0 8 1 1 9 1 8 28 1 3 10 0 9 10 0 2 11 0 11 29 0 8 11 0
		 0 18 0 1 17 0 12 24 0 9 16 0 13 14 0 8 19 0 15 25 0 12 15 0 16 20 0 17 21 0 16 17 1
		 18 22 0 17 34 1 19 23 0 18 19 1 19 27 1 20 14 0 21 13 0 20 21 1 22 12 0 21 35 1 23 15 0
		 22 23 1 23 26 1 24 13 0 25 14 0 24 25 1 26 20 1 25 26 1 27 16 1 26 27 1 28 9 1 27 28 1
		 29 10 0 28 29 1 30 3 0 29 30 1 31 5 0 30 31 1 32 7 0 31 32 1 33 1 0 32 33 1 34 18 1
		 33 34 1 35 22 1 34 35 1 35 24 1;
	setAttr -s 34 -ch 136 ".fc[0:33]" -type "polyFaces" 
		f 4 14 54 -19 -20
		mu 0 4 14 34 35 17
		f 4 1 58 -3 -7
		mu 0 4 2 36 37 4
		f 4 2 60 -4 -9
		mu 0 4 4 37 38 6
		f 4 3 62 -1 -11
		mu 0 4 6 38 40 8
		f 4 -12 -10 -8 -6
		mu 0 4 1 10 11 3
		f 4 10 4 6 8
		mu 0 4 12 0 2 13
		f 4 22 46 -27 -28
		mu 0 4 18 30 31 21
		f 4 5 15 -17 -14
		mu 0 4 1 3 16 15
		f 4 -2 17 18 56
		mu 0 4 36 2 17 35
		f 4 -5 12 19 -18
		mu 0 4 2 0 14 17
		f 4 0 64 63 -21
		mu 0 4 0 39 41 24
		f 4 13 23 30 -22
		mu 0 4 1 15 22 23
		f 4 -15 25 35 52
		mu 0 4 34 14 25 33
		f 4 -13 20 34 -26
		mu 0 4 14 0 24 25
		f 4 -31 28 38 -30
		mu 0 4 23 22 26 27
		f 4 -64 66 65 -32
		mu 0 4 24 41 42 28
		f 4 -35 31 42 -34
		mu 0 4 25 24 28 29
		f 4 -36 33 43 50
		mu 0 4 33 25 29 32
		f 4 -39 36 -25 -38
		mu 0 4 27 26 20 19
		f 4 -66 67 -23 -40
		mu 0 4 28 42 30 18
		f 4 -43 39 27 -42
		mu 0 4 29 28 18 21
		f 4 -44 41 26 48
		mu 0 4 32 29 21 31
		f 4 44 24 -46 -47
		mu 0 4 30 19 20 31
		f 4 -48 -49 45 -37
		mu 0 4 26 32 31 20
		f 4 -50 -51 47 -29
		mu 0 4 22 33 32 26
		f 4 -52 -53 49 -24
		mu 0 4 15 34 33 22
		f 4 -55 51 16 -54
		mu 0 4 35 34 15 16
		f 4 -56 -57 53 -16
		mu 0 4 3 36 35 16
		f 4 -59 55 7 -58
		mu 0 4 37 36 3 5
		f 4 -61 57 9 -60
		mu 0 4 38 37 5 7
		f 4 -63 59 11 -62
		mu 0 4 40 38 7 9
		f 4 -65 61 21 32
		mu 0 4 41 39 1 23
		f 4 -67 -33 29 40
		mu 0 4 42 41 23 27
		f 4 -68 -41 37 -45
		mu 0 4 30 42 27 19;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".pd[0]" -type "dataPolyComponent" Index_Data UV 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode transform -n "pCube5";
	rename -uid "2B2AF59D-4343-B119-B2A4-6887DF200741";
	setAttr ".t" -type "double3" -3 0 0 ;
	setAttr ".s" -type "double3" 1 0.5 6 ;
createNode transform -n "transform1" -p "pCube5";
	rename -uid "2B443ADE-46EC-94E6-0F1D-5F9E14A94805";
	setAttr ".v" no;
createNode mesh -n "pCubeShape5" -p "transform1";
	rename -uid "FC7455C4-4600-5844-F06B-2EB496314112";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr ".iog[0].og[0].gcl" -type "componentList" 1 "f[0:33]";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.75 0.375 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 43 ".uvst[0].uvsp[0:42]" -type "float2" 0.375 0 0.625 0 0.375
		 0.25 0.625 0.25 0.375 0.5 0.625 0.5 0.375 0.75 0.625 0.75 0.375 1 0.625 1 0.875 0
		 0.875 0.25 0.125 0 0.125 0.25 0.375 0 0.625 0 0.625 0.25 0.375 0.25 0.375 0 0.625
		 0 0.625 0 0.375 0 0.625 0 0.625 0 0.375 0 0.375 0 0.625 0 0.625 0 0.375 0 0.375 0
		 0.5 0 0.5 0 0.5 0 0.5 0 0.5 0 0.5 0.25 0.5 0.25 0.5 0.5 0.5 0.75 0.5 0 0.5 1 0.5
		 0 0.5 0;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 27 ".pt";
	setAttr ".pt[0]" -type "float3" 0 0.4937225 0 ;
	setAttr ".pt[1]" -type "float3" 0 0.4937225 0 ;
	setAttr ".pt[4]" -type "float3" 0 1.5130203 0 ;
	setAttr ".pt[5]" -type "float3" 0 1.5130203 0 ;
	setAttr ".pt[6]" -type "float3" 0 2.006743 0 ;
	setAttr ".pt[7]" -type "float3" 0 2.006743 0 ;
	setAttr ".pt[12]" -type "float3" 0.25555557 0.72492582 0 ;
	setAttr ".pt[13]" -type "float3" -0.25555557 0.72492582 0 ;
	setAttr ".pt[14]" -type "float3" -0.25555557 0.72492582 0 ;
	setAttr ".pt[15]" -type "float3" 0.25555557 0.72492582 0 ;
	setAttr ".pt[16]" -type "float3" 0 0.62136489 0 ;
	setAttr ".pt[17]" -type "float3" 0 0.62136489 0 ;
	setAttr ".pt[18]" -type "float3" 0 0.62136489 0 ;
	setAttr ".pt[19]" -type "float3" 0 0.62136489 0 ;
	setAttr ".pt[20]" -type "float3" -0.083333343 0.62136489 0 ;
	setAttr ".pt[21]" -type "float3" -0.083333343 0.62136489 0 ;
	setAttr ".pt[22]" -type "float3" 0.083333343 0.62136489 0 ;
	setAttr ".pt[23]" -type "float3" 0.083333343 0.62136489 0 ;
	setAttr ".pt[24]" -type "float3" 0 0.62136489 0 ;
	setAttr ".pt[25]" -type "float3" 0 0.62136489 0 ;
	setAttr ".pt[26]" -type "float3" 0 0.62136489 0 ;
	setAttr ".pt[27]" -type "float3" 0 0.62136489 0 ;
	setAttr ".pt[31]" -type "float3" 0 1.5130203 0 ;
	setAttr ".pt[32]" -type "float3" 0 2.006743 0 ;
	setAttr ".pt[33]" -type "float3" 0 0.4937225 0 ;
	setAttr ".pt[34]" -type "float3" 0 0.62136489 0 ;
	setAttr ".pt[35]" -type "float3" 0 0.62136489 0 ;
	setAttr -s 36 ".vt[0:35]"  -0.5 -0.5 0.5 0.5 -0.5 0.5 -0.5 0.5 0.5 0.5 0.5 0.5
		 -0.5 0.5 -0.5 0.5 0.5 -0.5 -0.5 -0.5 -0.5 0.5 -0.5 -0.5 -0.5 -0.5 0.5282644 0.5 -0.5 0.5282644
		 0.5 0.5 0.5282644 -0.5 0.5 0.5282644 -0.5 -2.5 0.5 0.5 -2.5 0.5 0.5 -2.5 0.5282644
		 -0.5 -2.5 0.5282644 0.5 -1.54166651 0.5282644 0.5 -1.54166651 0.5 -0.5 -1.54166651 0.5
		 -0.5 -1.54166651 0.5282644 0.5 -2.033279181 0.5282644 0.5 -2.033279181 0.5 -0.5 -2.033279181 0.5
		 -0.5 -2.033279181 0.5282644 0 -2.5 0.5 0 -2.5 0.5282644 0 -2.033279181 0.5282644
		 0 -1.54166651 0.5282644 0 -0.5 0.5282644 0 0.5 0.5282644 0 0.5 0.5 0 0.5 -0.5 0 -0.5 -0.5
		 0 -0.5 0.5 0 -1.54166651 0.5 0 -2.033279181 0.5;
	setAttr -s 68 ".ed[0:67]"  0 33 0 2 30 0 4 31 0 6 32 0 0 2 0 1 3 0 2 4 0
		 3 5 0 4 6 0 5 7 0 6 0 0 7 1 0 0 8 1 1 9 1 8 28 1 3 10 0 9 10 0 2 11 0 11 29 0 8 11 0
		 0 18 0 1 17 0 12 24 0 9 16 0 13 14 0 8 19 0 15 25 0 12 15 0 16 20 0 17 21 0 16 17 1
		 18 22 0 17 34 1 19 23 0 18 19 1 19 27 1 20 14 0 21 13 0 20 21 1 22 12 0 21 35 1 23 15 0
		 22 23 1 23 26 1 24 13 0 25 14 0 24 25 1 26 20 1 25 26 1 27 16 1 26 27 1 28 9 1 27 28 1
		 29 10 0 28 29 1 30 3 0 29 30 1 31 5 0 30 31 1 32 7 0 31 32 1 33 1 0 32 33 1 34 18 1
		 33 34 1 35 22 1 34 35 1 35 24 1;
	setAttr -s 34 -ch 136 ".fc[0:33]" -type "polyFaces" 
		f 4 14 54 -19 -20
		mu 0 4 14 34 35 17
		f 4 1 58 -3 -7
		mu 0 4 2 36 37 4
		f 4 2 60 -4 -9
		mu 0 4 4 37 38 6
		f 4 3 62 -1 -11
		mu 0 4 6 38 40 8
		f 4 -12 -10 -8 -6
		mu 0 4 1 10 11 3
		f 4 10 4 6 8
		mu 0 4 12 0 2 13
		f 4 22 46 -27 -28
		mu 0 4 18 30 31 21
		f 4 5 15 -17 -14
		mu 0 4 1 3 16 15
		f 4 -2 17 18 56
		mu 0 4 36 2 17 35
		f 4 -5 12 19 -18
		mu 0 4 2 0 14 17
		f 4 0 64 63 -21
		mu 0 4 0 39 41 24
		f 4 13 23 30 -22
		mu 0 4 1 15 22 23
		f 4 -15 25 35 52
		mu 0 4 34 14 25 33
		f 4 -13 20 34 -26
		mu 0 4 14 0 24 25
		f 4 -31 28 38 -30
		mu 0 4 23 22 26 27
		f 4 -64 66 65 -32
		mu 0 4 24 41 42 28
		f 4 -35 31 42 -34
		mu 0 4 25 24 28 29
		f 4 -36 33 43 50
		mu 0 4 33 25 29 32
		f 4 -39 36 -25 -38
		mu 0 4 27 26 20 19
		f 4 -66 67 -23 -40
		mu 0 4 28 42 30 18
		f 4 -43 39 27 -42
		mu 0 4 29 28 18 21
		f 4 -44 41 26 48
		mu 0 4 32 29 21 31
		f 4 44 24 -46 -47
		mu 0 4 30 19 20 31
		f 4 -48 -49 45 -37
		mu 0 4 26 32 31 20
		f 4 -50 -51 47 -29
		mu 0 4 22 33 32 26
		f 4 -52 -53 49 -24
		mu 0 4 15 34 33 22
		f 4 -55 51 16 -54
		mu 0 4 35 34 15 16
		f 4 -56 -57 53 -16
		mu 0 4 3 36 35 16
		f 4 -59 55 7 -58
		mu 0 4 37 36 3 5
		f 4 -61 57 9 -60
		mu 0 4 38 37 5 7
		f 4 -63 59 11 -62
		mu 0 4 40 38 7 9
		f 4 -65 61 21 32
		mu 0 4 41 39 1 23
		f 4 -67 -33 29 40
		mu 0 4 42 41 23 27
		f 4 -68 -41 37 -45
		mu 0 4 30 42 27 19;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".pd[0]" -type "dataPolyComponent" Index_Data UV 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode transform -n "pCube6";
	rename -uid "A3A5E568-41EB-F50A-8A01-9881E58584D5";
	setAttr ".rp" -type "double3" -2.5 0.033596277236938477 0.084793210029602051 ;
	setAttr ".sp" -type "double3" -2.5 0.033596277236938477 0.084793210029602051 ;
createNode mesh -n "pCube6Shape" -p "pCube6";
	rename -uid "F6C87B40-4A64-E25B-734C-578205C645CA";
	setAttr -k off ".v";
	setAttr -s 2 ".iog[0].og";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.74821427464485168 0.5 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 4 ".pt";
	setAttr ".pt[4]" -type "float3" -1.1175871e-08 1.8626451e-09 0 ;
	setAttr ".pt[6]" -type "float3" -3.7252903e-09 2.1420419e-08 0 ;
	setAttr ".pt[8]" -type "float3" -1.4901161e-08 0 -1.4901161e-08 ;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode lightLinker -s -n "lightLinker1";
	rename -uid "DC520D2A-4AF2-C9B5-F4E6-A986BA280BD4";
	setAttr -s 2 ".lnk";
	setAttr -s 2 ".slnk";
createNode shapeEditorManager -n "shapeEditorManager";
	rename -uid "3A7A2327-47AC-9771-ED7C-5498811A6439";
createNode poseInterpolatorManager -n "poseInterpolatorManager";
	rename -uid "FC6F76AE-4BF0-49F5-1E04-03B15C6E3B1D";
createNode displayLayerManager -n "layerManager";
	rename -uid "38A6B429-46C0-1307-FDB6-FBBB231C83FD";
createNode displayLayer -n "defaultLayer";
	rename -uid "2E6A165B-42DC-0D02-E42F-0F9353487304";
createNode renderLayerManager -n "renderLayerManager";
	rename -uid "A59CB967-4381-8172-4304-CEB0E96066C2";
createNode renderLayer -n "defaultRenderLayer";
	rename -uid "25D33387-4F40-90D8-E87A-E68CFC25BD69";
	setAttr ".g" yes;
createNode polyCube -n "polyCube1";
	rename -uid "704251FE-40A1-E32D-70A3-148B8B4CE36D";
	setAttr ".cuv" 4;
createNode polyExtrudeFace -n "polyExtrudeFace1";
	rename -uid "A02CB1A4-408A-954C-E841-FFAE087085CF";
	setAttr ".ics" -type "componentList" 1 "f[0]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 0.5 0 0 0 0 6 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 0 3 ;
	setAttr ".rs" 49243;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.5 -0.25 3 ;
	setAttr ".cbx" -type "double3" 0.5 0.25 3 ;
createNode polyExtrudeFace -n "polyExtrudeFace2";
	rename -uid "BD25EDD0-4C84-7432-B553-B6ABFBF7F694";
	setAttr ".ics" -type "componentList" 1 "f[6]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 0.5 0 0 0 0 6 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 -0.25 3.0847931 ;
	setAttr ".rs" 49257;
	setAttr ".lt" -type "double3" 0 0 1 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.5 -0.25 3 ;
	setAttr ".cbx" -type "double3" 0.5 -0.25 3.1695860624313354 ;
createNode polyTweak -n "polyTweak1";
	rename -uid "22F5E0EC-498F-49CD-290E-6384C5A68B84";
	setAttr ".uopa" yes;
	setAttr -s 5 ".tk";
	setAttr ".tk[8]" -type "float3" 0 0 0.028264353 ;
	setAttr ".tk[9]" -type "float3" 0 0 0.028264353 ;
	setAttr ".tk[10]" -type "float3" 0 0 0.028264353 ;
	setAttr ".tk[11]" -type "float3" 0 0 0.028264353 ;
createNode polySplitRing -n "polySplitRing1";
	rename -uid "55975D05-46F1-8B38-15CC-EF8DB89E2091";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 3 "e[20:21]" "e[23]" "e[25]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 0.5 0 0 0 0 6 0 0 0 0 1;
	setAttr ".wt" 0.52083325386047363;
	setAttr ".dr" no;
	setAttr ".re" 23;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing2";
	rename -uid "24B4E78D-487B-DA49-CF19-66852729CFA6";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 3 "e[28:29]" "e[31]" "e[33]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 0.5 0 0 0 0 6 0 0 0 0 1;
	setAttr ".wt" 0.51298701763153076;
	setAttr ".dr" no;
	setAttr ".re" 28;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing3";
	rename -uid "56AC7A50-47AE-5F8F-315A-1BBD5A236733";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 9 "e[0:3]" "e[14]" "e[18]" "e[22]" "e[26]" "e[32]" "e[35]" "e[40]" "e[43]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 0.5 0 0 0 0 6 0 0 0 0 1;
	setAttr ".wt" 0.33974060416221619;
	setAttr ".dr" no;
	setAttr ".re" 22;
	setAttr ".sma" 29.999999999999996;
	setAttr ".stp" 2;
	setAttr ".div" 1;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polyCloseBorder -n "polyCloseBorder1";
	rename -uid "BC2E9CA1-428F-618E-4F1A-359019918C74";
	setAttr ".ics" -type "componentList" 4 "e[0:1]" "e[4:5]" "e[12]" "e[17]";
createNode polyCloseBorder -n "polyCloseBorder2";
	rename -uid "2E4BFF8C-4599-0777-DF77-B892461811A6";
	setAttr ".ics" -type "componentList" 3 "e[0:3]" "e[47]" "e[49]";
createNode polySplit -n "polySplit1";
	rename -uid "E96874BC-40A1-81F0-0A08-4F88BD7D0160";
	setAttr -s 2 ".e[0:1]"  1 0;
	setAttr -s 2 ".d[0:1]"  -2147483647 -2147483599;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode script -n "uiConfigurationScriptNode";
	rename -uid "E0C6E427-4980-C738-917E-E398C9917177";
	setAttr ".b" -type "string" (
		"// Maya Mel UI Configuration File.\n//\n//  This script is machine generated.  Edit at your own risk.\n//\n//\n\nglobal string $gMainPane;\nif (`paneLayout -exists $gMainPane`) {\n\n\tglobal int $gUseScenePanelConfig;\n\tint    $useSceneConfig = $gUseScenePanelConfig;\n\tint    $nodeEditorPanelVisible = stringArrayContains(\"nodeEditorPanel1\", `getPanel -vis`);\n\tint    $nodeEditorWorkspaceControlOpen = (`workspaceControl -exists nodeEditorPanel1Window` && `workspaceControl -q -visible nodeEditorPanel1Window`);\n\tint    $menusOkayInPanels = `optionVar -q allowMenusInPanels`;\n\tint    $nVisPanes = `paneLayout -q -nvp $gMainPane`;\n\tint    $nPanes = 0;\n\tstring $editorName;\n\tstring $panelName;\n\tstring $itemFilterName;\n\tstring $panelConfig;\n\n\t//\n\t//  get current state of the UI\n\t//\n\tsceneUIReplacement -update $gMainPane;\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Top View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Top View\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"top\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n"
		+ "            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -controllers 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n"
		+ "            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 785\n            -height 327\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n"
		+ "\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Side View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Side View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"side\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n"
		+ "            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n"
		+ "            -sortTransparent 1\n            -controllers 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 785\n            -height 327\n"
		+ "            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Front View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Front View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"front\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n"
		+ "            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n"
		+ "            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -controllers 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n"
		+ "            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 785\n            -height 327\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Persp View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Persp View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"persp\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n"
		+ "            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n"
		+ "            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -controllers 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n"
		+ "            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 1581\n            -height 702\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"outlinerPanel\" (localizedPanelLabel(\"ToggledOutliner\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\toutlinerPanel -edit -l (localizedPanelLabel(\"ToggledOutliner\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\t$editorName = $panelName;\n        outlinerEditor -e \n            -showShapes 0\n            -showAssignedMaterials 0\n            -showTimeEditor 1\n            -showReferenceNodes 1\n            -showReferenceMembers 1\n            -showAttributes 0\n            -showConnected 0\n            -showAnimCurvesOnly 0\n            -showMuteInfo 0\n            -organizeByLayer 1\n            -organizeByClip 1\n            -showAnimLayerWeight 1\n            -autoExpandLayers 1\n            -autoExpand 0\n            -showDagOnly 1\n            -showAssets 1\n            -showContainedOnly 1\n            -showPublishedAsConnected 0\n            -showParentContainers 0\n            -showContainerContents 1\n            -ignoreDagHierarchy 0\n            -expandConnections 0\n            -showUpstreamCurves 1\n            -showUnitlessCurves 1\n            -showCompounds 1\n            -showLeafs 1\n            -showNumericAttrsOnly 0\n            -highlightActive 1\n            -autoSelectNewObjects 0\n            -doNotSelectNewObjects 0\n            -dropIsParent 1\n"
		+ "            -transmitFilters 0\n            -setFilter \"defaultSetFilter\" \n            -showSetMembers 1\n            -allowMultiSelection 1\n            -alwaysToggleSelect 0\n            -directSelect 0\n            -isSet 0\n            -isSetMember 0\n            -displayMode \"DAG\" \n            -expandObjects 0\n            -setsIgnoreFilters 1\n            -containersIgnoreFilters 0\n            -editAttrName 0\n            -showAttrValues 0\n            -highlightSecondary 0\n            -showUVAttrsOnly 0\n            -showTextureNodesOnly 0\n            -attrAlphaOrder \"default\" \n            -animLayerFilterOptions \"allAffecting\" \n            -sortOrder \"none\" \n            -longNames 0\n            -niceNames 1\n            -showNamespace 1\n            -showPinIcons 0\n            -mapMotionTrails 0\n            -ignoreHiddenAttribute 0\n            -ignoreOutlinerColor 0\n            -renderFilterVisible 0\n            -renderFilterIndex 0\n            -selectionOrder \"chronological\" \n            -expandAttribute 0\n            $editorName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"outlinerPanel\" (localizedPanelLabel(\"Outliner\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\toutlinerPanel -edit -l (localizedPanelLabel(\"Outliner\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        outlinerEditor -e \n            -showShapes 0\n            -showAssignedMaterials 0\n            -showTimeEditor 1\n            -showReferenceNodes 0\n            -showReferenceMembers 0\n            -showAttributes 0\n            -showConnected 0\n            -showAnimCurvesOnly 0\n            -showMuteInfo 0\n            -organizeByLayer 1\n            -organizeByClip 1\n            -showAnimLayerWeight 1\n            -autoExpandLayers 1\n            -autoExpand 0\n            -showDagOnly 1\n            -showAssets 1\n            -showContainedOnly 1\n            -showPublishedAsConnected 0\n            -showParentContainers 0\n            -showContainerContents 1\n            -ignoreDagHierarchy 0\n"
		+ "            -expandConnections 0\n            -showUpstreamCurves 1\n            -showUnitlessCurves 1\n            -showCompounds 1\n            -showLeafs 1\n            -showNumericAttrsOnly 0\n            -highlightActive 1\n            -autoSelectNewObjects 0\n            -doNotSelectNewObjects 0\n            -dropIsParent 1\n            -transmitFilters 0\n            -setFilter \"0\" \n            -showSetMembers 1\n            -allowMultiSelection 1\n            -alwaysToggleSelect 0\n            -directSelect 0\n            -displayMode \"DAG\" \n            -expandObjects 0\n            -setsIgnoreFilters 1\n            -containersIgnoreFilters 0\n            -editAttrName 0\n            -showAttrValues 0\n            -highlightSecondary 0\n            -showUVAttrsOnly 0\n            -showTextureNodesOnly 0\n            -attrAlphaOrder \"default\" \n            -animLayerFilterOptions \"allAffecting\" \n            -sortOrder \"none\" \n            -longNames 0\n            -niceNames 1\n            -showNamespace 1\n            -showPinIcons 0\n"
		+ "            -mapMotionTrails 0\n            -ignoreHiddenAttribute 0\n            -ignoreOutlinerColor 0\n            -renderFilterVisible 0\n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"graphEditor\" (localizedPanelLabel(\"Graph Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Graph Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showAssignedMaterials 0\n                -showTimeEditor 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -organizeByClip 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n"
		+ "                -autoExpand 1\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showParentContainers 1\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 0\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 1\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 1\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n"
		+ "                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 1\n                -mapMotionTrails 1\n                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                -renderFilterVisible 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"GraphEd\");\n            animCurveEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 1\n                -displayInfinities 0\n                -displayValues 0\n                -autoFit 1\n                -autoFitTime 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -showResults \"off\" \n"
		+ "                -showBufferCurves \"off\" \n                -smoothness \"fine\" \n                -resultSamples 1\n                -resultScreenSamples 0\n                -resultUpdate \"delayed\" \n                -showUpstreamCurves 1\n                -showCurveNames 0\n                -showActiveCurveNames 0\n                -stackedCurves 0\n                -stackedCurvesMin -1\n                -stackedCurvesMax 1\n                -stackedCurvesSpace 0.2\n                -displayNormalized 0\n                -preSelectionHighlight 0\n                -constrainDrag 0\n                -classicMode 1\n                -valueLinesToggle 1\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dopeSheetPanel\" (localizedPanelLabel(\"Dope Sheet\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dope Sheet\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n"
		+ "            outlinerEditor -e \n                -showShapes 1\n                -showAssignedMaterials 0\n                -showTimeEditor 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -organizeByClip 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showParentContainers 1\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 0\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n"
		+ "                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 1\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 1\n                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                -renderFilterVisible 0\n"
		+ "                $editorName;\n\n\t\t\t$editorName = ($panelName+\"DopeSheetEd\");\n            dopeSheetEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -displayValues 0\n                -autoFit 0\n                -autoFitTime 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -outliner \"dopeSheetPanel1OutlineEd\" \n                -showSummary 1\n                -showScene 0\n                -hierarchyBelow 0\n                -showTicks 1\n                -selectionWindow 0 0 0 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"timeEditorPanel\" (localizedPanelLabel(\"Time Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Time Editor\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"clipEditorPanel\" (localizedPanelLabel(\"Trax Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Trax Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = clipEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -displayValues 0\n                -autoFit 0\n                -autoFitTime 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -initialized 0\n                -manageSequencer 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"sequenceEditorPanel\" (localizedPanelLabel(\"Camera Sequencer\")) `;\n"
		+ "\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Camera Sequencer\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = sequenceEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -displayValues 0\n                -autoFit 0\n                -autoFitTime 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -initialized 0\n                -manageSequencer 1 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperGraphPanel\" (localizedPanelLabel(\"Hypergraph Hierarchy\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypergraph Hierarchy\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\n\t\t\t$editorName = ($panelName+\"HyperGraphEd\");\n            hyperGraph -e \n                -graphLayoutStyle \"hierarchicalLayout\" \n                -orientation \"horiz\" \n                -mergeConnections 1\n                -zoom 1\n                -animateTransition 0\n                -showRelationships 1\n                -showShapes 0\n                -showDeformers 0\n                -showExpressions 0\n                -showConstraints 0\n                -showConnectionFromSelected 0\n                -showConnectionToSelected 0\n                -showConstraintLabels 0\n                -showUnderworld 0\n                -showInvisible 0\n                -transitionFrames 1\n                -opaqueContainers 0\n                -freeform 0\n                -imagePosition 0 0 \n                -imageScale 1\n                -imageEnabled 0\n                -graphType \"DAG\" \n                -heatMapDisplay 0\n                -updateSelection 1\n                -updateNodeAdded 1\n                -useDrawOverrideColor 0\n                -limitGraphTraversal -1\n"
		+ "                -range 0 0 \n                -iconSize \"smallIcons\" \n                -showCachedConnections 0\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperShadePanel\" (localizedPanelLabel(\"Hypershade\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypershade\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"visorPanel\" (localizedPanelLabel(\"Visor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Visor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"nodeEditorPanel\" (localizedPanelLabel(\"Node Editor\")) `;\n\tif ($nodeEditorPanelVisible || $nodeEditorWorkspaceControlOpen) {\n"
		+ "\t\tif (\"\" == $panelName) {\n\t\t\tif ($useSceneConfig) {\n\t\t\t\t$panelName = `scriptedPanel -unParent  -type \"nodeEditorPanel\" -l (localizedPanelLabel(\"Node Editor\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"NodeEditorEd\");\n            nodeEditor -e \n                -allAttributes 0\n                -allNodes 0\n                -autoSizeNodes 1\n                -consistentNameSize 1\n                -createNodeCommand \"nodeEdCreateNodeCommand\" \n                -connectNodeOnCreation 0\n                -connectOnDrop 0\n                -copyConnectionsOnPaste 0\n                -connectionStyle \"bezier\" \n                -defaultPinnedState 0\n                -additiveGraphingMode 0\n                -settingsChangedCallback \"nodeEdSyncControls\" \n                -traversalDepthLimit -1\n                -keyPressCommand \"nodeEdKeyPressCommand\" \n                -nodeTitleMode \"name\" \n                -gridSnap 0\n                -gridVisibility 1\n                -crosshairOnEdgeDragging 0\n                -popupMenuScript \"nodeEdBuildPanelMenus\" \n"
		+ "                -showNamespace 1\n                -showShapes 1\n                -showSGShapes 0\n                -showTransforms 1\n                -useAssets 1\n                -syncedSelection 1\n                -extendToShapes 1\n                -editorMode \"default\" \n                -hasWatchpoint 0\n                $editorName;\n\t\t\t}\n\t\t} else {\n\t\t\t$label = `panel -q -label $panelName`;\n\t\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Node Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"NodeEditorEd\");\n            nodeEditor -e \n                -allAttributes 0\n                -allNodes 0\n                -autoSizeNodes 1\n                -consistentNameSize 1\n                -createNodeCommand \"nodeEdCreateNodeCommand\" \n                -connectNodeOnCreation 0\n                -connectOnDrop 0\n                -copyConnectionsOnPaste 0\n                -connectionStyle \"bezier\" \n                -defaultPinnedState 0\n                -additiveGraphingMode 0\n                -settingsChangedCallback \"nodeEdSyncControls\" \n"
		+ "                -traversalDepthLimit -1\n                -keyPressCommand \"nodeEdKeyPressCommand\" \n                -nodeTitleMode \"name\" \n                -gridSnap 0\n                -gridVisibility 1\n                -crosshairOnEdgeDragging 0\n                -popupMenuScript \"nodeEdBuildPanelMenus\" \n                -showNamespace 1\n                -showShapes 1\n                -showSGShapes 0\n                -showTransforms 1\n                -useAssets 1\n                -syncedSelection 1\n                -extendToShapes 1\n                -editorMode \"default\" \n                -hasWatchpoint 0\n                $editorName;\n\t\t\tif (!$useSceneConfig) {\n\t\t\t\tpanel -e -l $label $panelName;\n\t\t\t}\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"createNodePanel\" (localizedPanelLabel(\"Create Node\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Create Node\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n"
		+ "\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"polyTexturePlacementPanel\" (localizedPanelLabel(\"UV Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"UV Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"renderWindowPanel\" (localizedPanelLabel(\"Render View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Render View\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"shapePanel\" (localizedPanelLabel(\"Shape Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tshapePanel -edit -l (localizedPanelLabel(\"Shape Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n"
		+ "\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"posePanel\" (localizedPanelLabel(\"Pose Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tposePanel -edit -l (localizedPanelLabel(\"Pose Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynRelEdPanel\" (localizedPanelLabel(\"Dynamic Relationships\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dynamic Relationships\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"relationshipPanel\" (localizedPanelLabel(\"Relationship Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Relationship Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n"
		+ "\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"referenceEditorPanel\" (localizedPanelLabel(\"Reference Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Reference Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"componentEditorPanel\" (localizedPanelLabel(\"Component Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Component Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynPaintScriptedPanelType\" (localizedPanelLabel(\"Paint Effects\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Paint Effects\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"scriptEditorPanel\" (localizedPanelLabel(\"Script Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Script Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"profilerPanel\" (localizedPanelLabel(\"Profiler Tool\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Profiler Tool\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"contentBrowserPanel\" (localizedPanelLabel(\"Content Browser\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Content Browser\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\tif ($useSceneConfig) {\n        string $configName = `getPanel -cwl (localizedPanelLabel(\"Current Layout\"))`;\n        if (\"\" != $configName) {\n\t\t\tpanelConfiguration -edit -label (localizedPanelLabel(\"Current Layout\")) \n\t\t\t\t-userCreated false\n\t\t\t\t-defaultImage \"\"\n\t\t\t\t-image \"\"\n\t\t\t\t-sc false\n\t\t\t\t-configString \"global string $gMainPane; paneLayout -e -cn \\\"single\\\" -ps 1 100 100 $gMainPane;\"\n\t\t\t\t-removeAllPanels\n\t\t\t\t-ap false\n\t\t\t\t\t(localizedPanelLabel(\"Persp View\")) \n\t\t\t\t\t\"modelPanel\"\n"
		+ "\t\t\t\t\t\"$panelName = `modelPanel -unParent -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels `;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 0\\n    -headsUpDisplay 1\\n    -holdOuts 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 0\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 0\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 16384\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -depthOfFieldPreview 1\\n    -maxConstantTransparency 1\\n    -rendererName \\\"vp2Renderer\\\" \\n    -objectFilterShowInHUD 1\\n    -isFiltered 0\\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -controllers 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -imagePlane 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -particleInstancers 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -pluginShapes 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -clipGhosts 1\\n    -greasePencils 1\\n    -shadows 0\\n    -captureSequenceNumber -1\\n    -width 1581\\n    -height 702\\n    -sceneRenderFilter 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName;\\nmodelEditor -e \\n    -pluginObjects \\\"gpuCacheDisplayFilter\\\" 1 \\n    $editorName\"\n"
		+ "\t\t\t\t\t\"modelPanel -edit -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels  $panelName;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 0\\n    -headsUpDisplay 1\\n    -holdOuts 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 0\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 0\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 16384\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -depthOfFieldPreview 1\\n    -maxConstantTransparency 1\\n    -rendererName \\\"vp2Renderer\\\" \\n    -objectFilterShowInHUD 1\\n    -isFiltered 0\\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -controllers 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -imagePlane 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -particleInstancers 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -pluginShapes 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -clipGhosts 1\\n    -greasePencils 1\\n    -shadows 0\\n    -captureSequenceNumber -1\\n    -width 1581\\n    -height 702\\n    -sceneRenderFilter 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName;\\nmodelEditor -e \\n    -pluginObjects \\\"gpuCacheDisplayFilter\\\" 1 \\n    $editorName\"\n"
		+ "\t\t\t\t$configName;\n\n            setNamedPanelLayout (localizedPanelLabel(\"Current Layout\"));\n        }\n\n        panelHistory -e -clear mainPanelHistory;\n        sceneUIReplacement -clear;\n\t}\n\n\ngrid -spacing 5 -size 12 -divisions 5 -displayAxes yes -displayGridLines yes -displayDivisionLines yes -displayPerspectiveLabels no -displayOrthographicLabels no -displayAxesBold yes -perspectiveLabelPosition axis -orthographicLabelPosition edge;\nviewManip -drawCompass 0 -compassAngle 0 -frontParameters \"\" -homeParameters \"\" -selectionLockParameters \"\";\n}\n");
	setAttr ".st" 3;
createNode script -n "sceneConfigurationScriptNode";
	rename -uid "4255827C-41C3-A3C5-0BF9-0D906950DBAE";
	setAttr ".b" -type "string" "playbackOptions -min 1 -max 120 -ast 1 -aet 200 ";
	setAttr ".st" 6;
createNode polyCylinder -n "polyCylinder1";
	rename -uid "5F397BF8-4FBA-69C0-476C-28835A5F5F5B";
	setAttr ".sc" 1;
	setAttr ".cuv" 3;
createNode polyUnite -n "polyUnite1";
	rename -uid "99A3522F-4CC2-0F42-625B-79816DB9ACAB";
	setAttr -s 2 ".ip";
	setAttr -s 2 ".im";
createNode groupId -n "groupId1";
	rename -uid "99AE31F2-4ACF-9FCA-807F-C7B248DD40A6";
	setAttr ".ihi" 0;
createNode groupId -n "groupId2";
	rename -uid "E8D5E802-457A-B90A-EE44-63B3C33B66CC";
	setAttr ".ihi" 0;
createNode groupId -n "groupId3";
	rename -uid "4A5983B7-4675-F1CC-B400-C8A5BA7A357C";
	setAttr ".ihi" 0;
createNode groupId -n "groupId4";
	rename -uid "9839E77E-4B70-C280-8637-5E9FC8B9D6C4";
	setAttr ".ihi" 0;
createNode groupId -n "groupId5";
	rename -uid "9F504AC4-47EF-368B-EC35-ADB925985BF5";
	setAttr ".ihi" 0;
createNode groupParts -n "groupParts1";
	rename -uid "3D97ACC8-4591-97AB-5375-7584321482AD";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "f[0:67]";
createNode polyMergeVert -n "polyMergeVert1";
	rename -uid "3E28D35F-4ABC-0954-7138-858789E6E988";
	setAttr ".ics" -type "componentList" 2 "vtx[4]" "vtx[41]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".d" 1;
	setAttr ".am" yes;
createNode polyMergeVert -n "polyMergeVert2";
	rename -uid "A3096997-4C7E-BC36-4752-83A4FA718431";
	setAttr ".ics" -type "componentList" 2 "vtx[6]" "vtx[42]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".am" yes;
createNode polyMergeVert -n "polyMergeVert3";
	rename -uid "FD13F17A-43F3-A349-41BF-699D247B928C";
	setAttr ".ics" -type "componentList" 9 "vtx[0]" "vtx[2]" "vtx[8]" "vtx[11]" "vtx[18:19]" "vtx[37]" "vtx[39]" "vtx[43:44]" "vtx[50:51]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".am" yes;
createNode polyTweakUV -n "polyTweakUV1";
	rename -uid "B1DC830E-4B57-87F6-C0C1-10BC83EA3A10";
	setAttr ".uopa" yes;
	setAttr -s 86 ".uvtk[0:85]" -type "float2" 0.29196575 0 0.20446289 0
		 0.29196575 0 0.20446289 0 0.29196575 0 0.20446289 0 0.29196575 0 0.20446289 0 0.29196575
		 0 0.20446289 0 0.11695999 0 0.11695999 0 0.37946859 0 0.37946859 0 0.29196575 0 0.20446289
		 0 0.20446289 0 0.29196575 0 0.29196575 0 0.20446289 0 0.20446289 0 0.29196575 0 0.20446289
		 0 0.20446289 0 0.29196575 0 0.29196575 0 0.20446289 0 0.20446289 0 0.29196575 0 0.29196575
		 0 0.2482143 0 0.2482143 0 0.2482143 0 0.2482143 0 0.2482143 0 0.2482143 0 0.2482143
		 0 0.2482143 0 0.2482143 0 0.2482143 0 0.2482143 0 0.2482143 0 0.2482143 0 -0.20624858
		 0 -0.24999999 0 -0.24999999 0 -0.20624858 0 -0.20624858 0 -0.24999999 0 -0.24999999
		 0 -0.20624858 0 -0.24999999 0 -0.20624858 0 -0.24999999 0 -0.20624858 0 -0.29375142
		 0 -0.38125429 0 -0.38125429 0 -0.29375142 0 -0.11874574 0 -0.20624858 0 -0.11874574
		 0 -0.20624858 0 -0.24999999 0 -0.24999999 0 -0.20624858 0 -0.29375142 0 -0.29375142
		 0 -0.24999999 0 -0.24999999 0 -0.20624858 0 -0.29375142 0 -0.29375142 0 -0.20624858
		 0 -0.24999999 0 -0.29375142 0 -0.29375142 0 -0.24999999 0 -0.20624858 0 -0.20624858
		 0 -0.24999999 0 -0.29375142 0 -0.29375142 0 -0.29375142 0 -0.29375142 0 -0.29375142
		 0;
select -ne :time1;
	setAttr ".o" 1;
	setAttr ".unw" 1;
select -ne :hardwareRenderingGlobals;
	setAttr ".otfna" -type "stringArray" 22 "NURBS Curves" "NURBS Surfaces" "Polygons" "Subdiv Surface" "Particles" "Particle Instance" "Fluids" "Strokes" "Image Planes" "UI" "Lights" "Cameras" "Locators" "Joints" "IK Handles" "Deformers" "Motion Trails" "Components" "Hair Systems" "Follicles" "Misc. UI" "Ornaments"  ;
	setAttr ".otfva" -type "Int32Array" 22 0 1 1 1 1 1
		 1 1 1 0 0 0 0 0 0 0 0 0
		 0 0 0 0 ;
	setAttr ".fprt" yes;
select -ne :renderPartition;
	setAttr -s 2 ".st";
select -ne :renderGlobalsList1;
select -ne :defaultShaderList1;
	setAttr -s 4 ".s";
select -ne :postProcessList1;
	setAttr -s 2 ".p";
select -ne :defaultRenderingList1;
select -ne :initialShadingGroup;
	setAttr -s 9 ".dsm";
	setAttr ".ro" yes;
	setAttr -s 5 ".gn";
select -ne :initialParticleSE;
	setAttr ".ro" yes;
select -ne :defaultRenderGlobals;
	setAttr ".ren" -type "string" "arnold";
select -ne :defaultResolution;
	setAttr ".pa" 1;
select -ne :hardwareRenderGlobals;
	setAttr ".ctrs" 256;
	setAttr ".btrs" 512;
select -ne :ikSystem;
	setAttr -s 4 ".sol";
connectAttr "polySplitRing3.out" "pCubeShape1.i";
connectAttr "polyCloseBorder1.out" "pCubeShape2.i";
connectAttr "polySplit1.out" "pCubeShape3.i";
connectAttr "polyCylinder1.out" "pCylinderShape1.i";
connectAttr "groupId1.id" "pCubeShape4.iog.og[0].gid";
connectAttr ":initialShadingGroup.mwc" "pCubeShape4.iog.og[0].gco";
connectAttr "groupId2.id" "pCubeShape4.ciog.cog[0].cgid";
connectAttr "groupId3.id" "pCubeShape5.iog.og[0].gid";
connectAttr ":initialShadingGroup.mwc" "pCubeShape5.iog.og[0].gco";
connectAttr "groupId4.id" "pCubeShape5.ciog.cog[0].cgid";
connectAttr "polyTweakUV1.out" "pCube6Shape.i";
connectAttr "groupId5.id" "pCube6Shape.iog.og[0].gid";
connectAttr ":initialShadingGroup.mwc" "pCube6Shape.iog.og[0].gco";
connectAttr "polyTweakUV1.uvtk[0]" "pCube6Shape.uvst[0].uvtw";
relationship "link" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
connectAttr "layerManager.dli[0]" "defaultLayer.id";
connectAttr "renderLayerManager.rlmi[0]" "defaultRenderLayer.rlid";
connectAttr "polyCube1.out" "polyExtrudeFace1.ip";
connectAttr "pCubeShape1.wm" "polyExtrudeFace1.mp";
connectAttr "polyTweak1.out" "polyExtrudeFace2.ip";
connectAttr "pCubeShape1.wm" "polyExtrudeFace2.mp";
connectAttr "polyExtrudeFace1.out" "polyTweak1.ip";
connectAttr "polyExtrudeFace2.out" "polySplitRing1.ip";
connectAttr "pCubeShape1.wm" "polySplitRing1.mp";
connectAttr "polySplitRing1.out" "polySplitRing2.ip";
connectAttr "pCubeShape1.wm" "polySplitRing2.mp";
connectAttr "polySplitRing2.out" "polySplitRing3.ip";
connectAttr "pCubeShape1.wm" "polySplitRing3.mp";
connectAttr "polySurfaceShape1.o" "polyCloseBorder1.ip";
connectAttr "polySurfaceShape2.o" "polyCloseBorder2.ip";
connectAttr "polyCloseBorder2.out" "polySplit1.ip";
connectAttr "pCubeShape4.o" "polyUnite1.ip[0]";
connectAttr "pCubeShape5.o" "polyUnite1.ip[1]";
connectAttr "pCubeShape4.wm" "polyUnite1.im[0]";
connectAttr "pCubeShape5.wm" "polyUnite1.im[1]";
connectAttr "polyUnite1.out" "groupParts1.ig";
connectAttr "groupId5.id" "groupParts1.gi";
connectAttr "groupParts1.og" "polyMergeVert1.ip";
connectAttr "pCube6Shape.wm" "polyMergeVert1.mp";
connectAttr "polyMergeVert1.out" "polyMergeVert2.ip";
connectAttr "pCube6Shape.wm" "polyMergeVert2.mp";
connectAttr "polyMergeVert2.out" "polyMergeVert3.ip";
connectAttr "pCube6Shape.wm" "polyMergeVert3.mp";
connectAttr "polyMergeVert3.out" "polyTweakUV1.ip";
connectAttr "defaultRenderLayer.msg" ":defaultRenderingList1.r" -na;
connectAttr "pCubeShape1.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape2.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape3.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCylinderShape1.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape4.iog.og[0]" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape4.ciog.cog[0]" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape5.iog.og[0]" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape5.ciog.cog[0]" ":initialShadingGroup.dsm" -na;
connectAttr "pCube6Shape.iog.og[0]" ":initialShadingGroup.dsm" -na;
connectAttr "groupId1.msg" ":initialShadingGroup.gn" -na;
connectAttr "groupId2.msg" ":initialShadingGroup.gn" -na;
connectAttr "groupId3.msg" ":initialShadingGroup.gn" -na;
connectAttr "groupId4.msg" ":initialShadingGroup.gn" -na;
connectAttr "groupId5.msg" ":initialShadingGroup.gn" -na;
// End of ModularMarketTent.ma
