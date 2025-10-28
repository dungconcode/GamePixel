VAR questAccepted = false

Già Làng: Người lạ, ngươi đến thật đúng lúc.
Bọn quái vật đang phá hoại cánh đồng. Ngươi có thể giúp ta giết 5 con không?
+ [Để ta suy nghĩ] 
    Già Làng: Được thôi, nhưng nhớ quay lại sớm.
    -> END
+ [Nhận nhiệm vụ] 
    ~ questAccepted = true
    Già Làng: Tốt lắm! Hãy tiêu diệt 5 con quái.
    -> END

