import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { LearningAreaService } from '../../../services/learning-area.service';

@Component({
  selector: 'app-create-learning-area',
  templateUrl: './learning-area-create.component.html',
  styleUrls: ['./learning-area-create.component.css']
})
export class LearningAreaCreateComponent {
  fg: FormGroup;
  editingId: any;

  constructor(private fb: FormBuilder, private activatedRoute: ActivatedRoute, private router: Router,
    private learningAreaService: LearningAreaService) {
    this.fg = this.fb.group({
      "Name": [null, Validators.required],
      "Description": [null],
    });
  }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.editingId = params['id'];
      if (this.editingId) {
        this.getServerData();
      }
    });
  }

  async getServerData() {
    let response = await this.learningAreaService.Get(this.editingId);
    this.fg.patchValue(response);
  }

  onSave() {
    if (this.fg.valid) {
      if (this.editingId) {
        this.learningAreaService.Replace(this.editingId, this.fg.value)
          .then(response => this.router.navigate(['/learning-area-details'], { queryParams: { id: this.editingId } }));
      } else {
        this.learningAreaService.Create(this.fg.value)
          .then(response => this.router.navigate(['/data-management']));
      }
    }
  }

}
