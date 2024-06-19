import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrWorkerComponent } from './or-worker.component';

describe('OrWorkerComponent', () => {
  let component: OrWorkerComponent;
  let fixture: ComponentFixture<OrWorkerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OrWorkerComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(OrWorkerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
