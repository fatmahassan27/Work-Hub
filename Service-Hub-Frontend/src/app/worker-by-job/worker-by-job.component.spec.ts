import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkerByJobComponent } from './worker-by-job.component';

describe('WorkerByJobComponent', () => {
  let component: WorkerByJobComponent;
  let fixture: ComponentFixture<WorkerByJobComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WorkerByJobComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(WorkerByJobComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
