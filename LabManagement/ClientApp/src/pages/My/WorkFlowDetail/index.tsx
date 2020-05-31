import {
  Card,
  Statistic,
  Descriptions,
  Steps,
  List,
  Tabs,
  Collapse,
} from 'antd';
import 'antd/dist/antd.css';
import { GridContent, PageHeaderWrapper, RouteContext } from '@ant-design/pro-layout';
import React, { FC, useEffect } from 'react';
import { connect, Dispatch, WorkFlowDetailStateType } from 'umi';
import styles from './style.less';
import { IChemical, IFinancialForm, IDeclarationForm, IWorkFlow } from '@/models/entity';

const { Step } = Steps;
const extra = (
  <div className={styles.moreInfo}>
    <Statistic title="状态" value="待审批" />
    <Statistic title="订单金额" value={568.08} prefix="¥" />
  </div>
);

const Description = (param: {
  workflow?: IWorkFlow
}) => (
    <RouteContext.Consumer>
      {(value) => {
        const { isMobile } = value;
        return (
          <Descriptions className={styles.headerList} size="small" column={isMobile ? 1 : 2}>
            <Descriptions.Item label="创建人">{param.workflow?.uname}</Descriptions.Item>
            <Descriptions.Item label="简介">{param.workflow?.description}</Descriptions.Item>
            <Descriptions.Item label="创建时间">{param.workflow?.startTime}</Descriptions.Item>
            <Descriptions.Item label="备注">{param.workflow?.state}</Descriptions.Item>
          </Descriptions>
        )
      }}
    </RouteContext.Consumer>
  );
const ChemicalList = (param: {
  loading: boolean;
  chemicals: IChemical[];
}) => (<List
  size="large"
  rowKey="id"
  loading={param.loading}
  dataSource={param.chemicals}
  renderItem={(item) => (
    <List.Item
    >
      <List.Item.Meta
        title={<span>{item.name}</span>}
        description={<div>
          <span>{item.amount}</span>
          <span>{item.unitmeasurement}</span>
        </div>}
      />
    </List.Item>
  )}
/>
  )
const FinancialFormCollapse = (param: {
  loading: boolean;
  financialForms: IFinancialForm[];
}) => (
    <Collapse>
      {param.financialForms.map(u => (
        <Collapse.Panel header={'收款方：' + u.receiver} key={u.id as number}>
          <Descriptions bordered>
            <Descriptions.Item label='申请人'>{u.uname}</Descriptions.Item>
            <Descriptions.Item label='实验室编号'>{u.lid}</Descriptions.Item>
            <Descriptions.Item label='当前状态'>{u.state}</Descriptions.Item>
            <Descriptions.Item label='金额'>{u.price?.toString() + '元'}</Descriptions.Item>
            <Descriptions.Item label='提交时间'>{u.stime}</Descriptions.Item>
            <Descriptions.Item label='收款方'>{u.receiver}</Descriptions.Item>
          </Descriptions>
        </Collapse.Panel>
      ))}
    </Collapse>
  )
const DeclarationFormDescription = (param: {
  declarationForm: IDeclarationForm;
}) => (
    <Descriptions bordered>
      <Descriptions.Item label='申请人'>{param.declarationForm.uname}</Descriptions.Item>
      <Descriptions.Item label='实验室编号'>{param.declarationForm.lid}</Descriptions.Item>
      <Descriptions.Item label='当前状态'>{param.declarationForm.state}</Descriptions.Item>
      <Descriptions.Item label='理由' span={2}>{param.declarationForm.reason}</Descriptions.Item>
      <Descriptions.Item label='处理人'>{param.declarationForm.hname}</Descriptions.Item>
      <Descriptions.Item label='提交时间'>{param.declarationForm.stime}</Descriptions.Item>
    </Descriptions>
  )
interface WorkFlowDetailProps {
  loading: boolean;
  workFlowDetailState: WorkFlowDetailStateType;
  dispatch: Dispatch;
  id: number;
}
const WorkFlowDetail: FC<
  WorkFlowDetailProps
> = (props) => {
  const { dispatch, loading } = props;
  useEffect(() => {
    if (props.workFlowDetailState.currentWorkFlow === undefined) {
      dispatch({
        type: 'workFlowDetail/fetchWorkFlow',
        payload: props.id
      });
    }
  }, [1]);
  const { currentWorkFlow, declarationForm, financialForms } = props.workFlowDetailState || {};
  const currentStep = ()=>{
    if(currentWorkFlow?.state === 'declearing'){
      return 1;
    }
    if(currentWorkFlow?.state === 'security' || currentWorkFlow?.state === 'securityOk'){
      return 2;
    }
    if(currentWorkFlow?.state === 'financial'){
      return 2;
    }
    if(currentWorkFlow?.state === 'inPurchasing'){
      return 3;
    }
    if(currentWorkFlow?.state === 'inStore'){
      return 4;
    }
  }
  return (
    <PageHeaderWrapper
      title={'单号: ' + props.id.toString()}
      className={styles.pageHeader}
      content={<Description workflow={currentWorkFlow} />}
      extraContent={extra}
    >
      <div className={styles.main}>
        <GridContent>
          <Card title="流程进度" style={{ marginBottom: 24 }}>
            <RouteContext.Consumer>
              {({ isMobile }) => (
                <Steps
                  direction={isMobile ? 'vertical' : 'horizontal'}
                  current={currentStep()}
                >
                  <Step title="提交申请" />
                  <Step title="实验室老师审批" />
                  <Step title="财务审批" />
                  <Step title="采购中" />
                  <Step title="完成" />
                </Steps>
              )}
            </RouteContext.Consumer>
          </Card>
          <Card
            bordered={false}
          >
            <Tabs>
              <Tabs.TabPane tab='危险品列表' key='chemicals'>
                <ChemicalList
                  loading={loading}
                  chemicals={currentWorkFlow?.chemicals || []}
                />
              </Tabs.TabPane>
              <Tabs.TabPane tab='财务表单' key='financials'>
                <FinancialFormCollapse loading={loading} financialForms={financialForms} />
              </Tabs.TabPane>
              <Tabs.TabPane tab='申请表' key='declaration'>
                <DeclarationFormDescription declarationForm={declarationForm || {}} />
              </Tabs.TabPane>
            </Tabs>
          </Card>
        </GridContent>
      </div>
    </PageHeaderWrapper>
  );
}
const mapStateToProps = ({
  loading,
  workFlowDetail
}: {
  workFlowDetail: WorkFlowDetailStateType;
  loading: {
    models: { [key: string]: boolean };
  };
}, ownProps: any) => {
  const id = Number.parseInt(ownProps.match.params.workflowid);
  return {
    loading: loading.models.currentWorkFlow,
    workFlowDetailState: workFlowDetail,
    id
  }
}
export default connect(
  mapStateToProps
)(WorkFlowDetail);