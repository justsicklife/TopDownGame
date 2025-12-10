-> start_phone

=== start_phone ===
전화가 울리고 있다. 
    * [전화를 받는다.]
    -> start_phone_a
    
    * [전화가 울리게 둔다.]
    -> start_phone_b
    
=== start_phone_a ===
여보세요? 
난 너를 알아 너도 나를 알고 있어
    * [너가 누군데]
        나의 이름은 맥거핀 이야
        -> END
    * [내가 누군데?]
        그건 너가 알아내야지
        -> END

-> END

=== start_phone_b === 
전화벨 소리가 계속 울릴거같다.
-> END