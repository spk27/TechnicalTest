import { Component, TemplateRef, OnInit } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ApplicantsClient, StepsClient,
  ApplicantDto, StepDto, PriorityLevelDto,
  CreateApplicantCommand, UpdateApplicantCommand,
  CreateStepCommand, UpdateStepCommand, UpdateStepDetailCommand
} from '../web-api-client';

@Component({
  selector: 'app-applicant-component',
  templateUrl: './applicant.component.html',
  styleUrls: ['./applicant.component.scss']
})
export class ApplicantComponent implements OnInit {
  debug = false;
  lists: ApplicantDto[];
  priorityLevels: PriorityLevelDto[];
  selectedList: ApplicantDto;
  selectedItem: StepDto;
  newListEditor: any = {};
  listOptionsEditor: any = {};
  itemDetailsEditor: any = {};
  newListModalRef: BsModalRef;
  listOptionsModalRef: BsModalRef;
  deleteListModalRef: BsModalRef;
  itemDetailsModalRef: BsModalRef;

  constructor(
    private listsClient: ApplicantsClient,
    private itemsClient: StepsClient,
    private modalService: BsModalService
  ) {}

  ngOnInit(): void {
    this.listsClient.get().subscribe(
      result => {
        this.lists = result.lists;
        this.priorityLevels = result.priorityLevels;
        if (this.lists.length) {
          this.selectedList = this.lists[0];
        }
      },
      error => console.error(error)
    );
  }

  // Lists
  remainingItems(list: ApplicantDto): number {
    return list.steps.filter(t => !t.done).length;
  }

  showNewListModal(template: TemplateRef<any>): void {
    this.newListModalRef = this.modalService.show(template);
    setTimeout(() => document.getElementById('title').focus(), 250);
  }

  newListCancelled(): void {
    this.newListModalRef.hide();
    this.newListEditor = {};
  }

  addList(): void {
    const list = {
      id: 0,
      title: this.newListEditor.title,
      steps: []
    } as ApplicantDto;

    this.listsClient.create(list as CreateApplicantCommand).subscribe(
      result => {
        list.id = result;
        this.lists.push(list);
        this.selectedList = list;
        this.newListModalRef.hide();
        this.newListEditor = {};
      },
      error => {
        const errors = JSON.parse(error.response);

        if (errors && errors.Title) {
          this.newListEditor.error = errors.Title[0];
        }

        setTimeout(() => document.getElementById('title').focus(), 250);
      }
    );
  }

  showListOptionsModal(template: TemplateRef<any>) {
    this.listOptionsEditor = {
      id: this.selectedList.id,
      title: this.selectedList.title
    };

    this.listOptionsModalRef = this.modalService.show(template);
  }

  updateListOptions() {
    const list = this.listOptionsEditor as UpdateApplicantCommand;
    this.listsClient.update(this.selectedList.id, list).subscribe(
      () => {
        (this.selectedList.title = this.listOptionsEditor.title),
          this.listOptionsModalRef.hide();
        this.listOptionsEditor = {};
      },
      error => console.error(error)
    );
  }

  confirmDeleteList(template: TemplateRef<any>) {
    this.listOptionsModalRef.hide();
    this.deleteListModalRef = this.modalService.show(template);
  }

  deleteListConfirmed(): void {
    this.listsClient.delete(this.selectedList.id).subscribe(
      () => {
        this.deleteListModalRef.hide();
        this.lists = this.lists.filter(t => t.id !== this.selectedList.id);
        this.selectedList = this.lists.length ? this.lists[0] : null;
      },
      error => console.error(error)
    );
  }

  // Items
  showItemDetailsModal(template: TemplateRef<any>, item: StepDto): void {
    this.selectedItem = item;
    this.itemDetailsEditor = {
      ...this.selectedItem
    };

    this.itemDetailsModalRef = this.modalService.show(template);
  }

  updateItemDetails(): void {
    const item = this.itemDetailsEditor as UpdateStepDetailCommand;
    this.itemsClient.updateItemDetails(this.selectedItem.id, item).subscribe(
      () => {
        if (this.selectedItem.applicantId !== this.itemDetailsEditor.listId) {
          this.selectedList.steps = this.selectedList.steps.filter(
            i => i.id !== this.selectedItem.id
          );
          const listIndex = this.lists.findIndex(
            l => l.id === this.itemDetailsEditor.listId
          );
          this.selectedItem.applicantId = this.itemDetailsEditor.listId;
          this.lists[listIndex].steps.push(this.selectedItem);
        }

        this.selectedItem.priority = this.itemDetailsEditor.priority;
        this.selectedItem.note = this.itemDetailsEditor.note;
        this.itemDetailsModalRef.hide();
        this.itemDetailsEditor = {};
      },
      error => console.error(error)
    );
  }

  addItem() {
    const item = {
      id: 0,
      applicantId: this.selectedList.id,
      priority: this.priorityLevels[0].value,
      title: '',
      done: false
    } as StepDto;

    this.selectedList.steps.push(item);
    const index = this.selectedList.steps.length - 1;
    this.editItem(item, 'itemTitle' + index);
  }

  editItem(item: StepDto, inputId: string): void {
    this.selectedItem = item;
    setTimeout(() => document.getElementById(inputId).focus(), 100);
  }

  updateItem(item: StepDto, pressedEnter: boolean = false): void {
    const isNewItem = item.id === 0;

    if (!item.title.trim()) {
      this.deleteItem(item);
      return;
    }

    if (item.id === 0) {
      this.itemsClient
        .create({ title: item.title, applicantId: this.selectedList.id
          } as CreateStepCommand)
        .subscribe(
          result => {
            item.id = result;
          },
          error => console.error(error)
        );
    } else {
      this.itemsClient.update(item.id, item as UpdateStepCommand).subscribe(
        () => console.log('Update succeeded.'),
        error => console.error(error)
      );
    }

    this.selectedItem = null;

    if (isNewItem && pressedEnter) {
      setTimeout(() => this.addItem(), 250);
    }
  }

  deleteItem(item: StepDto) {
    if (this.itemDetailsModalRef) {
      this.itemDetailsModalRef.hide();
    }

    if (item.id === 0) {
      const itemIndex = this.selectedList.steps.indexOf(this.selectedItem);
      this.selectedList.steps.splice(itemIndex, 1);
    } else {
      this.itemsClient.delete(item.id).subscribe(
        () =>
          (this.selectedList.steps = this.selectedList.steps.filter(
            t => t.id !== item.id
          )),
        error => console.error(error)
      );
    }
  }
}
