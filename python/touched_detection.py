#!/usr/bin/env python
# coding: utf-8

# In[1]:


import numpy as np
import cv2
import math

fingertips_touched_flag = np.zeros((10,1))

def touching_detection(tips, hand_mask, depth_image, draw_image=None):
    '''
    by Yuan-Syun ye on 2019/06/17.
    
    To detect if the fingertips are touching or not, our algorithm analyzes a 7x7 patch centered on the fingertip's contour position.
    Each patch of pixels centered on the fingertip's contour position. Each patch is split into S, the set of pixels within the hand + finger mask,
    and T, the set of pixels outside the mask. The estimated height of the finger is then given by max(Zs|s C S) - min(Zt|t C T)
    
    To confirm contact with the surface, the algorithm applies a simple pair of hysteresis thresholds - a fingertip is declared as touching
    the surface if the smoothed fingertip height descends below 10 mm, and declared to have left the surface if its height laster ascends past 15 mm.
    '''
    global fingertips_touched_flag
    kernal_size = 7
    touch_height = 10#0.02
    untouch_height = 15#0.025
    
    # debug image
    if(draw_image is not None):
        touched_text = "touched"
        text_size = 0.5
        touched_color = (0, 255, 0)
        touched_image = draw_image.copy()
    
    max_height, max_width =  hand_mask.shape
#     print('max width: %d, height: %d'%(max_width, max_height))
#     print('test access: %d'%(hand_mask[170, 223]))
    for index, tip in enumerate(tips):
        
        # this tip is not tracking
        if(tip[0] == -1):
            fingertips_touched_flag[index] = False
            continue
            
        # the min hight within the hand+finger mask
        Zs = (0,0)
        tip_height = -999
        
        # the max height outside the mask.
        Zt = (0,0)
        surface_height = 999

#         print ('tip[%d] = (%d, %d)' % (i, tip[0], tip[1]))
        kernal_range = math.floor(kernal_size/2)
        for h in range(-kernal_range, kernal_range+ 1, 1):
            for w in range(-kernal_range, kernal_range + 1, 1):
                (u, v) = (int(tip[1]+w), int(tip[0]+h))
                
                # check the bounder
                if(u < 0 or u >= max_height):
                    continue
                if(v < 0 or v >= max_width):
                    continue

                # Debug20191031 SkinImg is [0,255]
                if (hand_mask[u, v] == True or hand_mask[u, v] == 255):
                    if(depth_image[u, v] > tip_height):
                        Zs = (u, v)
                        tip_height = depth_image[u,v]
                else:
                    if(depth_image[u, v] < surface_height):
                        Zt = (u,v )
                        surface_height = depth_image[u,v]
                        
#                 if(draw_image is not None):
#                     touched_image[u, v]=touched_color

        #20191031 Debug : tip/surface not change, put these code outside the for loop 
        if(tip_height!= -999 and surface_height!= 999 and (tip_height - surface_height) < touch_height):
            fingertips_touched_flag[index] = True
            print(tip_height,surface_height,(tip_height - surface_height))
#             print(depth_image[int(tip[1])-kernal_range:int(tip[1])+kernal_range, int(tip[0])-kernal_range:int(tip[0])+kernal_range] )
#                 print('finger %d touched'%(index))
        if(tip_height == -999 or surface_height == 999 or (tip_height - surface_height) >= untouch_height):
            fingertips_touched_flag[index] = False
#             print(tip_height,surface_height,(tip_height - surface_height))
#     print(fingertips_touched_flag)

    # debug image
    if(draw_image is not None):
        for i, touched in enumerate(fingertips_touched_flag):
            if(touched == True):
#                 print('tips[%d], tips size: %d, %d'%(i, tips.shape[0], tips.shape[1]))
                pos = (int(tips[i][0]), int(tips[i][1]))
                print('touched pos:', pos)
                cv2.circle(touched_image, pos, 5 , touched_color , 3)
                cv2.putText(img=touched_image, text=touched_text, org=pos, fontFace=cv2.FONT_HERSHEY_SIMPLEX, fontScale=text_size, color=touched_color)
    return fingertips_touched_flag, touched_image


# In[2]:




# In[ ]:




