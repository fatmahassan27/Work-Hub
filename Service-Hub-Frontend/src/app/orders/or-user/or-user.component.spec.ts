import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrUserComponent } from './or-user.component';

describe('OrUserComponent', () => {
  let component: OrUserComponent;
  let fixture: ComponentFixture<OrUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OrUserComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(OrUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
