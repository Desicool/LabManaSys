import {
  Card,
  Badge,
  Tabs,
  Empty,
} from 'antd';
import { GridContent, PageHeaderWrapper } from '@ant-design/pro-layout';
import React, { FC, useEffect, useState } from 'react';
import { connect, Dispatch, FormToProcessState, history, UserModelState, IRole } from 'umi';
import styles from './style.less';
import DeclarationProcess from './component/DeclarationProcess';
import FinancialProcess from './component/FinancialProcess';
import ClaimProcess from './component/ClaimProcess';
import { NotifyWorkFlowComponent } from './component/WorkFlowNotify';
import { ClaimFormList } from './component/ClaimFormNotify';

interface FormToProcessProps {
  loading: boolean;
  myFormToProcess: FormToProcessState;
  authority: IRole[];
  dispatch: Dispatch
}

const FormToProcess: FC<FormToProcessProps> = (props) => {
  const { dispatch, myFormToProcess, authority } = props;
  useEffect(() => {
    dispatch({
      type: 'myFormToProcess/fetchMessage',
    });
    dispatch({
      type: 'myFormToProcess/fetchNotify'
    })
  }, [1]);
  const [tabKey, setTabKey] = useState<string>('declaration');
  const declearTodo = myFormToProcess.msg?.dform.filter(u => u.state === 'InProcess').length;
  const financialTodo = myFormToProcess.msg?.fform.filter(u => u.state === 'InProcess').length;
  const claimTodo = myFormToProcess.msg?.cform.filter(u => u.state === 'InProcess').length;
  const tabList = [
    {
      key: 'declaration',
      tab:
        <Badge count={declearTodo} offset={[10, 0]}><p>申报</p></Badge>,
    },
    {
      key: 'financial',
      tab: <Badge count={financialTodo} offset={[10, 0]}><p>财务申请</p></Badge>
    },
    {
      key: 'claimform',
      tab: <Badge count={claimTodo} offset={[10, 0]}><p>领用申请</p></Badge>
    },
  ];
  const contentList = {
    declaration: <DeclarationProcess />,
    claimform: <ClaimProcess />,
    financial: <FinancialProcess />
  };

  const TodoList = () => {
    if (declearTodo === undefined || financialTodo === undefined || claimTodo === undefined)
      return null;
    if (declearTodo + financialTodo + claimTodo == 0) {
      return (
        <Card title='需要处理的申请'
          style={{ marginBottom: 24 }}
          bordered={false}
          tabList={tabList}
          onTabChange={setTabKey}
          activeTabKey={tabKey}
        >
          <Empty/>
        </Card>
      );
    }
    return (
      <Card title='需要处理的申请'
        style={{ marginBottom: 24 }}
        bordered={false}
        tabList={tabList}
        onTabChange={setTabKey}
        activeTabKey={tabKey}
      >
        {contentList[tabKey]}
      </Card>
    )
  }
  return (
    <PageHeaderWrapper
      title="我的待办"
      className={styles.pageHeader}
    >
      <div className={styles.main}>
        <GridContent>
          {
            authority.find(u => u.roleName === 'Student') === undefined ?
              <TodoList /> : null
          }
          <div className={styles.standardList}>
            <Card
              className={styles.listCard}
              bordered={false}
              title="有状态更新的项目"
              style={{ marginTop: 24 }}
              bodyStyle={{ padding: '0 32px 40px 32px' }}
            >
              <Tabs>
                <Tabs.TabPane tab="流程" key="workflownotify">
                  <NotifyWorkFlowComponent notify={myFormToProcess.notify} dispatch={dispatch} />
                </Tabs.TabPane>
                <Tabs.TabPane tab="申领进度" key="chemicalnotify">
                  <ClaimFormList cform={myFormToProcess.notify?.cform} dispatch={dispatch} />
                </Tabs.TabPane>
              </Tabs>
            </Card>
          </div>
        </GridContent>
      </div>
    </PageHeaderWrapper>
  );
}

export default connect(
  ({
    myFormToProcess,
    loading,
    user,
  }: {
    myFormToProcess: FormToProcessState;
    user: UserModelState;
    loading: {
      effects: { [key: string]: boolean };
    };
  }) => ({
    myFormToProcess,
    authority: user.roles || [],
    loading: loading.effects['myFormToProcess/fetchAdvanced'],
  }),
)(FormToProcess);
